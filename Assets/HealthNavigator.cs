using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthNavigator : MonoBehaviour {

    [SerializeField] private RawImage m_RowImage;

    private GameObject m_HealthPlus;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        m_HealthPlus = GameObject.FindWithTag("HealthPlus");

        if (m_HealthPlus) {
            Navigate();
        } else {
            // There is no health so we just hide arrow.
            m_RowImage.enabled = false;
        }

    }

    private void Navigate() {

        float xDiff = m_HealthPlus.transform.position.x - transform.position.x;
        float zDiff = m_HealthPlus.transform.position.z - transform.position.z;

        float angle = Mathf.Atan(zDiff / xDiff) * Mathf.Rad2Deg;

        // Tangent only returns an angle from -90 to +90.  we need to check if its behind us and adjust.
        if (xDiff < 0) {
            if (zDiff >= 0)
                angle += 180f;
            else
                angle -= 180f;
        }

        // -90 degrees is default rotation that actually equals 0 degrees in x0y system
        transform.localRotation = Quaternion.Euler(0f, 0f, angle - 90);

        m_RowImage.enabled = true;

    }

}
