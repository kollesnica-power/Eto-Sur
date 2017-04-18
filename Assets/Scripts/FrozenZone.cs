using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenZone : MonoBehaviour {

    [SerializeField] private float slowMultiplier;

    private GameObject m_FireFX;
    private GameObject m_Light;

    private float m_TimeToLive = 1f;
    private List<PlayerMovement> m_PlayerMovementArray;

    // Use this for initialization
    void Start () {

        Destroy(gameObject, m_TimeToLive);
        m_PlayerMovementArray = new List<PlayerMovement>();


    }

    public void SetTTL(float TTL) {

        m_TimeToLive = TTL;

    }

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Player")) {

            // If player in zone he will slowed and we rememer PlayerMovement instanse.
            PlayerMovement pm = other.gameObject.GetComponent<PlayerMovement>();
            m_PlayerMovementArray.Add(pm);

            if (pm) {
                pm.DecreaseSpeed(slowMultiplier);
            }

        } else if (other.CompareTag("Bonfire")) {
            TurnOffFire(other.gameObject);
        }

    }

    private void OnTriggerExit(Collider other) {

        if (other.CompareTag("Player")) {

            // If player out of zone he gets his default speed.
            PlayerMovement pm = other.gameObject.GetComponent<PlayerMovement>();
            m_PlayerMovementArray.Remove(pm);

            if (pm) {
                pm.IncreaseSpeed(slowMultiplier);
            }

        }

    }

    private void OnDestroy() {

        if (m_PlayerMovementArray.Count > 0) {
            // Setting default speed because zone doesn't exit.
            foreach (PlayerMovement item in m_PlayerMovementArray) {
                item.IncreaseSpeed(slowMultiplier);
            }

        }

        if (m_FireFX) {
            m_FireFX.SetActive(true);
        }

        if (m_Light) {
            m_Light.SetActive(true);
        }
        
    }

    private void TurnOffFire(GameObject bonfire) {

        // Put out the bonfire and remember instanses to turn them on when zone will destoyed.

        m_FireFX = bonfire.transform.Find("Fire").gameObject;
        m_Light = bonfire.transform.Find("Light").gameObject;

        if (m_FireFX) {
            m_FireFX.SetActive(false);
        }

        if (m_Light) {
            m_Light.SetActive(false);
        }

    }

}
