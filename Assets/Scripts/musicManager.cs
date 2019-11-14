using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musicManager : MonoBehaviour 
{
    void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1) {
            // this is obviously a copy and should not be the level.
            Destroy(this.gameObject);
        }
    }
        
        public int mainMenuIndex = 0;
        public int worldIndex = 1;
        public int world2Index = 2;
        public int world3Index = 3;

        public List<AudioClip> songs = new List<AudioClip>();

        private AudioSource aud;


    void Start() {
        aud = this.GetComponent<AudioSource>();
        aud.clip = songs[0];
    }

    public bool debug = true;

    void Update() {
        if(debug) {
            if(Input.GetKeyDown(KeyCode.Alpha9)) {
                Debug.Log("starting Next Level");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                StartCoroutine(StartNextLevel());
            }
        }
    }
    public IEnumerator StartNextLevel() {
        yield return new WaitForSeconds(0.5f);
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        if(currentIndex == mainMenuIndex) {
            aud.clip = songs[0];
            aud.Play();
        }
        else if(currentIndex == worldIndex) {
            aud.clip = songs[1];
            aud.Play();
        }
        else if(currentIndex == world2Index) {
            aud.clip = songs[2];
            aud.Play();
        }
        else if(currentIndex == world3Index) {
            aud.clip = songs[3];
            aud.Play();
        }
    }

}