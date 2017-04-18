using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] private GameObject m_Target;

    private Vector3 offset;
    private Quaternion rotationOffset;

	// Use this for initialization
	void Start () {

        offset = m_Target.transform.position - transform.position;
        rotationOffset = transform.rotation;

    }
	
	// Update is called once per frame
	void LateUpdate () {

        transform.position = m_Target.transform.position - offset;

        transform.rotation = m_Target.transform.rotation * rotationOffset;


    }
}
