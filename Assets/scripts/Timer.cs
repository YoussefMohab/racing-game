using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
  public TextMeshProUGUI timerText;
  public TextMeshProUGUI scoreText;
  public TextMeshProUGUI finishText;
  private bool hasFinished = false;
  [SerializeField] GameObject player;


        static float timer;
// Start is called before the first frame update
    void Start()
    {

    }
// Update is called once per frame
 void Update()
    {
        timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);

        string time = string.Format("{0:0}:{1:00}", minutes, seconds);

        timerText.text = time;
    }

//     private void OnTriggerEnter (Collider other){
//    scoreText.text = "Score " + timerText.text;
//             timer = 0.0f;
// }
 private void OnTriggerEnter(Collider other)
    {
        hasFinished = true;

        // Display finish text
        finishText.text = "Game Over!";

        // Set score text
        scoreText.text = "Score: " + timerText.text;

        // Set timer to zero
        timer = 0.0f;

        // Set player speed to zero
        //  GameObject carGameObject = GameObject.FindGameObjectWithTag("Player");
        
                // Get the Rigidbody component from the car GameObject
                Rigidbody carRigidbody = player.GetComponent<Rigidbody>();

                // Set player speed to zero
                carRigidbody.velocity = Vector3.zero;
                carRigidbody.angularVelocity = Vector3.zero;

                player.GetComponent<carcontroller>().speed = 0;
                

                // Disable car controls
                player.GetComponent<carcontroller>().SetControlsEnabled(false);
          
    }

   
}
