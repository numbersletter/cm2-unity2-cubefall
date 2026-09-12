using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    //Add Variables here
    float speed = 10.0f;
    public UnityEvent gameEnd;
    // Update is called once per frame
    void Update()
    {
        //Add Movement checking here
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += UnityEngine.Vector3.left * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += UnityEngine.Vector3.right * Time.deltaTime * speed;
        }

    }

    //Add OnCollisionEnter() here
    void OnCollisionEnter(Collision Other)
    {
        if (Other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            gameEnd.Invoke();
        }
    }

}
