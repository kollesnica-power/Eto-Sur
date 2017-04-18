using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    [SerializeField] private ParticleSystem explosionPrefab;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float damage = 20f;

    private ParticleSystem explosion;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {

        explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as ParticleSystem;
        explosion.Play();

        DoDamage();

        Destroy(gameObject);

    }

    private void DoDamage() {

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, mask);

        

        for (int i = 0; i < colliders.Length; i++) {

            PlayerHealth playerHealth = colliders[i].GetComponent<PlayerHealth>();

            if (playerHealth) {

                playerHealth.TakeDamage(damage);

            }

        }

    }

}
