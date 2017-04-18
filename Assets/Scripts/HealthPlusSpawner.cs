using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlusSpawner : MonoBehaviour {

    [SerializeField] private float m_SpawnDelay = 10f;
    [SerializeField] private GameObject m_PlusPrefab;

    private float m_Timer;
    private GameObject m_LastPlus;

    private void OnEnable() {

        m_Timer = 5f;

    }

    private void OnDisable() {

        ClearPluses();

    }

    // Use this for initialization
    void Start () {

        m_Timer = 5f;

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

        Vector3 pos = new Vector3(Random.Range(-22f, 23f), 2f, Random.Range(-22f, 23f));

        m_LastPlus = Instantiate(m_PlusPrefab, transform.TransformDirection(pos), Quaternion.identity);

        m_LastPlus.GetComponent<HealthPlus>().SetTTL(m_SpawnDelay);

    }

    private void ClearPluses() {

        Destroy(m_LastPlus);

    }

}
