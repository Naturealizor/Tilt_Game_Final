using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class marbleMovement : MonoBehaviour
{
    Vector3 dir;
    Vector3 calibratedDir;
    Rigidbody rb;

    public float speed = 1.0f;        
    public int score = 0;
    public bool canJump = false;
    public float jumpSpeed = 5f;
    public bool debug = true;
    bool grounded = true;
    
    public Transform arrowIndicator;
    public TextMeshProUGUI scoreText;
    public GameObject jumpButton;
    public GameObject nextLevelButton;
    public GameObject winScreen;
    public Camera mainCam;      // assign this in Start() to the "MainCamera"

    // Start is called before the first frame update
    void Start() 
    {
        rb = this.GetComponent<Rigidbody>();
        Calibrate();
        Vector3 position = this.transform.position;
        startPos = position;
        winScreen.SetActive(false);
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        //mainCam = GameObject.Find("MainCamera").GetComponent<Camera>();
        jumpButton.SetActive(canJump);
    }

    // Update is called once per frame
    void Update() {
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
    public void nextLevel() {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
         if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
         {
            SceneManager.LoadScene(nextSceneIndex);
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
           


         // USED TO MAKE ARROW KEYS MOVE PLAYER

        //  if (Input.GetKey(KeyCode.LeftArrow))
    //  {
    //      transform.position += Vector3.left * speed * Time.deltaTime;
    //  }
    //  if (Input.GetKey(KeyCode.RightArrow))
    //  {
    //      transform.position += Vector3.right * speed * Time.deltaTime;
    //  }
    //  if (Input.GetKey(KeyCode.UpArrow))
    //  {
    //      transform.position += Vector3.forward * speed * Time.deltaTime;
    //  }
    //  if (Input.GetKey(KeyCode.DownArrow))
    //  {
    //      transform.position += Vector3.back * speed * Time.deltaTime;
    //  }
    