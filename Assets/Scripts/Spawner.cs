using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
    * Spawner class is responsible for spawning zombies at regular intervals.
    * It uses utilizes coroutine methods calling itself over a certain period
    * of time to spawn (instantiate) zombies. Perhaps we can adjust this
    * spawn rate as a difficulty variable.
*/
public class Spawner : MonoBehaviour
{
    [SerializeField]
    public GameObject zombie;


    // Spawn frequency of the zombies
    public float spawnTime = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnZombie());
    }

    /*
        * Co routine method to spawn zombies at regular intervals.
    */
    public IEnumerator spawnZombie()
    {
        while(true)
        {
            GameObject zombieCopy = Instantiate(zombie, 
                                                new Vector3(Random.Range(-27f, 28f), 21f, 0f), 
                                                transform.rotation);
            yield return new WaitForSeconds(spawnTime);

        }
    }
    
}

