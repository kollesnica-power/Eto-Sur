using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenZoneSpawner : MonoBehaviour {

    [SerializeField] private float m_SpawnDelay = 5f;
    [SerializeField] private GameObject m_ZonePrefab;

    private float m_Timer;

    private void OnEnable() {

        m_Timer = 0f;

    }

    // Use this for initialization
    void Start () {

        m_Timer = 0f;

	}
	
	// Update is called once per frame
	void Update () {

        if (m_Timer >= m_SpawnDelay) {
            m_Timer = 0f;
            Spawn();
        }

        m_Timer += Time.deltaTime;

    }

    private void Spawn() {

        Vector3 pos = new Vector3(Random.Range(-13f, 14f), 0f, Random.Range(-13f, 14f));

        GameObject obj =  Instantiate(m_ZonePrefab, transform.TransformDirection(pos), Quaternion.identity);

        obj.GetComponent<FrozenZone>().SetTTL(m_SpawnDelay);

    }

}
