using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class marbleMovement : MonoBehaviour
{
    Vector3 dir;
    Vector3 calibratedDir;
    Rigidbody rb;
    public float speed = 1.0f;
    public bool debug = true;
    public Transform arrowIndicator;
    public TextMeshProUGUI scoreText;
    public int score = 0;
    public bool canJump = false;
    // public GameObject jumpButton;
    public Camera mainCam;      // assign this in Start() to the "MainCamera"
    


    // Start is called before the first frame update
    void Start() {
        rb = this.GetComponent<Rigidbody>();
        Calibrate();
         scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        //  mainCam = GameObject.Find("MainCamera").GetComponent<Camera>();
        //  jumpButton.SetActive(canJump);
    }

    // Update is called once per frame
    void Update() {
        dir.x = Input.acceleration.x - calibratedDir.x;      // x to x
        dir.z = Input.acceleration.y - calibratedDir.z;      // y to z

        if(debug) {
            Debug.DrawRay(this.transform.position, dir * 2, Color.red, 0.5f);
        }
    }

    void LateUpdate() {
        arrowIndicator.rotation = Quaternion.LookRotation(dir, Vector3.up);
        Vector3 scale = Vector3.one;
        scale.z = dir.magnitude * 2;
        arrowIndicator.localScale = scale * 2;
    }

    void FixedUpdate() {
        rb.AddForce(dir * speed);
    }

    // void ResetPosition() {
    //     this.transform.position
    // }
     public void Calibrate() {
       calibratedDir.x = Input.acceleration.x;
       calibratedDir.x = Input.acceleration.y;
       Debug.Log("Calibrated Dir = " + calibratedDir);
   }

    public float jumpSpeed = 5f;
    public void Jump() {
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }

    public void Teleport() {
        this.transform.Translate(arrowIndicator.forward * 2, Space.World);
    }

    // private Camera mainCam;
    bool grounded = false;
   void OnCollisionEnter(Collision other) {
       // NEED TO FINISH THIS PART
    //    if(other.gameObject.name == "JumpPowerup") {
    //        canJump = true;
    //        jumpButton.SetActive(canJump);
    //        Destroy(other.gameObject);
    //    }

        // NEED TO FIGURE OUT HOW TO CHANGE GROUND
        if(other.gameObject.CompareTag("Ground")) {
            grounded = true;
        }
        if(other.gameObject.CompareTag("Coin")) {
            score += 1000;
            scoreText.text = "Score = " + score;
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("CustomCam")) {
            mainCam.gameObject.SetActive(false);
            other.transform.GetChild(0).gameObject.SetActive(true);
        }
   }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("CustomCam")) {
            mainCam.gameObject.SetActive(true);
            other.transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }
}

// TO DO LIST:
    // Fix the camera triggers, main cam does not work if other cam is active.
    // Finish the slider
    // Finish the 2 levels
    // Look into teleporters
    // Add jump powerups and force platforms
    // 
