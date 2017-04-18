using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLaser : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private Transform transformA;
    [SerializeField] private Transform transformB;

    private Vector3 currentDest;
    private Vector3 posA;
    private Vector3 posB;
    private Vector3 m_StartPosition;

    private void Awake() {

        m_StartPosition = transform.position;

    }

    private void OnEnable() {

        transform.position = m_StartPosition;

    }


    // Use this for initialization
    void Start () {

        posA = transformA.localPosition;
        posB = transformB.localPosition;
        currentDest = posB;

	}
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(transform.localPosition, currentDest) < 0.1) {
            ChangeDirection();
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, currentDest, speed * Time.deltaTime);

	}

    private void ChangeDirection() {

        currentDest = (currentDest != posA) ? posA : posB;

    }

}
