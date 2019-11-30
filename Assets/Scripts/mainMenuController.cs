using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuController : MonoBehaviour
{
    public void SwitchScene(int index = 0) {
        SceneManager.LoadScene(index);
    }

    public void QuitGame() {
        Debug.Log("Quitting the game!");
        Application.Quit();
    }
}