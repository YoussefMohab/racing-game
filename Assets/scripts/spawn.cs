using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> checkPoints;
    [SerializeField] Vector3 respawnPosition;
    [SerializeField] Vector3 respawnRotation;
    [SerializeField] float dead;

    void Start()
    {
        respawnPosition = player.transform.position;
        respawnRotation = player.transform.eulerAngles;
    }

    void Update()
    {

         // Check if the 'R' key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }

        if (player.transform.position.y < -dead)
        {
            RespawnPlayer();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        respawnPosition = player.transform.position;
        Destroy(other.gameObject);
          if (other.gameObject.CompareTag("Road"))
        {
            RespawnPlayer();
        }
    }

    void RespawnPlayer()
    {
        Rigidbody carRigidbody = player.GetComponent<Rigidbody>();

        player.transform.position = respawnPosition;
        player.transform.eulerAngles = respawnRotation; // Set the rotation
         // Reset the car's velocity to zero
    carRigidbody.velocity = Vector3.zero;
    carRigidbody.angularVelocity = Vector3.zero;
    }
}
