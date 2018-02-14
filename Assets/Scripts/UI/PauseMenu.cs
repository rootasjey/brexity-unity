using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseUI;
    private bool paused = false;

    private void Start() {
        pauseUI = GameObject.Find("PauseMenu");
        pauseUI.SetActive(false);
    }

    private void Update() {
        if (Input.GetButtonDown("Pause")) {
            paused = !paused;
        }

        if (paused) {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        } else {
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Resume() {
        paused = false;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Quit() {
        Application.Quit();
    }
}
