using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    //Add Variables here
    float speed = 10.0f;
    public UnityEvent gameEnd;
    [SerializeField] private GameObject BrokenCube;
    [SerializeField] private float explosionForce = 0f;
    [SerializeField] private float explosionRadius = 2f;


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

    //  If collide with Enemy, delete player and invoke gameEnd event
    //  TODO: make a proper game end screen with score and have a restart 
    //  button. 
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            breakIntoPieces();
            gameEnd.Invoke();
        }
    }   

    private void breakIntoPieces()
    {
        // spawn broken up version of player cube
        GameObject brokenCube = Instantiate(BrokenCube, transform.position, transform.rotation);
        
        // This big complicated code moves the broken cubes down to the floor (we don't want to spawn them in mid air)
        Renderer[] pieceRenderers = brokenCube.GetComponentsInChildren<Renderer>();
        if (pieceRenderers.Length > 0 && Physics.Raycast(transform.position + Vector3.up * 100f, 
                                                        Vector3.down,
                                                        out RaycastHit floorHit, Mathf.Infinity, Physics.DefaultRaycastLayers,
                                                        QueryTriggerInteraction.Ignore)
                                                        )
        {
            // make a bounding box covering ALL the pieces of the broken  cube
            Bounds pieceBounds = pieceRenderers[0].bounds;
            foreach (Renderer pieceRenderer in pieceRenderers)
            {
                pieceBounds.Encapsulate(pieceRenderer.bounds);
            }

            // move the broken cube up or down so that the bottom of the bounding box 
            // is at the same height as the floor hit point
            brokenCube.transform.position += Vector3.up * (floorHit.point.y - pieceBounds.min.y);
        }


        // Add an explosive effect on the cubes
        Rigidbody[] rigidPieces = brokenCube.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody piece in rigidPieces)
        {
            // applies physics push from the center of the impact point outward
            piece.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        // delete original player cube
        Destroy(gameObject);

    }

}
