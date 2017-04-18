using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamaging : MonoBehaviour {

    [SerializeField] private float damage = 20f;

	// Use this for initialization
	void Start () {

    }

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }

    }

}
