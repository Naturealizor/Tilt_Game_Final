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

     // ADDED FOR ARROWWS
    // private float turnDirection = 0;
    // public float rotSpeed = 10;
    // ADDED FOR ARROWWS

    public float speed = 1.0f;        //1.0f
    public int score = 0;
    public bool canJump = false;
    public float jumpSpeed = 5f;
    public bool debug = true;
    bool grounded = true;
    
    public Transform arrowIndicator;
    public TextMeshProUGUI scoreText;
    public GameObject jumpButton;
    public GameObject winScreen;
    public Camera mainCam;      // assign this in Start() to the "MainCamera"

    // Start is called before the first frame update
    void Start() {
        rb = this.GetComponent<Rigidbody>();
        Calibrate();
        startPos = this.transform.position;
        winScreen.SetActive(false);
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        // mainCam = GameObject.Find("MainCamera").GetComponent<Camera>();
        // jumpButton.SetActive(canJump);
    }

    // Update is called once per frame
    void Update() {
         if (Input.GetKey(KeyCode.LeftArrow))
     {
         transform.position += Vector3.left * speed * Time.deltaTime;
     }
     if (Input.GetKey(KeyCode.RightArrow))
     {
         transform.position += Vector3.right * speed * Time.deltaTime;
     }
     if (Input.GetKey(KeyCode.UpArrow))
     {
         transform.position += Vector3.forward * speed * Time.deltaTime;
     }
     if (Input.GetKey(KeyCode.DownArrow))
     {
         transform.position += Vector3.back * speed * Time.deltaTime;
     }
     
        dir.x = Input.acceleration.x - calibratedDir.x;      // x to x
        dir.z = Input.acceleration.y - calibratedDir.z;      // y to z

        if(debug) {
            Debug.DrawRay(this.transform.position, dir * 2, Color.red, 0.5f);
        }if(score >= 1000) {
            Win();
        }
    }

    void Win() {
        if(score>= 1000) {
            winScreen.SetActive(true);
        } else {
            winScreen.SetActive(false);
        }
    }

    void LateUpdate() {
        arrowIndicator.rotation = Quaternion.LookRotation(dir, Vector3.up);
        Vector3 scale = Vector3.one;
        scale.z = dir.magnitude * 2;
        arrowIndicator.localScale = scale * 2;

        if(this.transform.position.y <= 0) {
            ResetPosition();
        }
    }

    Vector3 startPos;
    void ResetPosition() {
        this.transform.position = startPos;
        rb.velocity = Vector3.zero;     // this stops all movement
    }

    void FixedUpdate() {
        rb.AddForce(dir * speed);
    }

    public void Calibrate() {
       calibratedDir.x = Input.acceleration.x;
       calibratedDir.x = Input.acceleration.y;
       Debug.Log("Calibrated Dir = " + calibratedDir);
   }

    public void Jump() {
        if(grounded && canJump) {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }

    public void Teleport() {
        this.transform.Translate(arrowIndicator.forward * 2, Space.World);
    }

    void OnCollisionEnter() { 
       grounded = true;
       }

    void OnCollisionExit() {
        grounded = false;        
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "JumpPowerup") {
               canJump = true;
               jumpButton.SetActive(canJump);
               Destroy(other.gameObject);
           }
        if(other.gameObject.CompareTag("Coin")) {
           score += 250;
           scoreText.text = "Score = " + score;
           Destroy(other.gameObject);
           } 
       if(other.gameObject.CompareTag("CustomCam")) {
           mainCam.gameObject.SetActive(false);other.transform.GetChild(0).gameObject.SetActive(true);
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


 // NEED TO FINISH THIS PART
           


        