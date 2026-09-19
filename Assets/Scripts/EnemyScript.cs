using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public static event Action EnemyPassedKillZone;

    public GameObject impactParticlePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Handles collision with the killzone (despawning the enemy cubes)
    // Plays a little particle effect when the enemy cube is destroyed
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("KillZone"))
        {
            EnemyPassedKillZone?.Invoke();          // ? means invoke only if there are subscribers to the event

            Vector3 contactPoint = other.ClosestPoint(transform.position);
            GameObject impactParticleObject = Instantiate(impactParticlePrefab, contactPoint, Quaternion.identity);
            ParticleSystem impactParticle = impactParticleObject.GetComponent<ParticleSystem>();
            impactParticle.Play();
            Destroy(gameObject);
        }
    }
}
