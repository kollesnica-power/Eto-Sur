using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour {

    [SerializeField] private float m_LaunchDelay = 2f;
    [SerializeField] private GameObject m_BombPrefab;
    [SerializeField] private GameManager m_GM;

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

        if (m_Timer >= m_LaunchDelay) {
            m_Timer = 0f;
            Launch();
        }

        m_Timer += Time.deltaTime;

	}

    private void Launch() {

        
        GameObject playerToLaunch = ChoseRandomplayer();

        if (playerToLaunch) {
            Vector3 pos = new Vector3(playerToLaunch.transform.position.x, transform.position.y, playerToLaunch.transform.position.z);
            Instantiate(m_BombPrefab, pos, Quaternion.Euler(90f, 0f, 0f)); 
        }

    }

    // Choose player to launch by random.
    private GameObject ChoseRandomplayer() {

        int alivePlayers = 0;
        GameObject alivePlayerObject = null;

        for (int i = 0; i < m_GM.m_Players.Length; i++) {
            if (m_GM.m_Players[i].m_Instance.activeSelf) {
                alivePlayers++;
                alivePlayerObject = m_GM.m_Players[i].m_Instance;
            }
        }

        if (alivePlayers == 0) {
            return null;
        } else if (alivePlayers == 1) {
            return alivePlayerObject;
        }

        int playerNumber = Random.Range(0, alivePlayers);

        return m_GM.m_Players[playerNumber].m_Instance;

    }

}
