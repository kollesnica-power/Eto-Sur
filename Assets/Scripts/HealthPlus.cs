using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlus : MonoBehaviour {

    [SerializeField] float m_Speed = 100f;
    [SerializeField] bool isRotate = false;

    private float m_TimeToLive;

    public void SetTTL(float TTL) {

        m_TimeToLive = TTL;

    }

	// Use this for initialization
	void Start () {

        Destroy(gameObject, m_TimeToLive);

    }
	
	// Update is called once per frame
	void Update () {

        if (isRotate) {
            transform.Rotate(Vector3.up * Time.deltaTime * m_Speed); 
        }

	}

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Enviroment") || other.CompareTag("Bonfire")) {

            transform.position = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z + 2f);

        }

    }

}
