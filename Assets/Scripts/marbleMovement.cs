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
    // public float rotSpeed = 60f;


    // Start is called before the first frame update
    void Start() {
        rb = this.GetComponent<Rigidbody>();
        Calibrate();
         scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        // Vector3 dir = Vector3.zero;

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

    // void OnCollisionEnter(Collision other) {
    //     if (other.gameObject.CompareTag("Coin")){
    //         Debug.Log("Congratz");
    //     }
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
    // This is for the score UI, need to change the compare tags to coins
    // Also see if grounded 

    bool grounded = false;

   void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Ground")) {
            grounded = true;
        }
        else if(other.gameObject.CompareTag("Coin")) {
            score += 1000;
            scoreText.text = "Score = " + score;
            Destroy(other.gameObject);
        }
   }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Ground")) {
            grounded = false;
        }
    }
}
