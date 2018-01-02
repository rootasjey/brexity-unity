using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    private GameObject _mainMenu;
    private GameObject _optionsMenu;

    private void Start() {
        _mainMenu = GameObject.Find("MainMenu");
        _optionsMenu = GameObject.Find("OptionsMenu");

        _optionsMenu.SetActive(false);
    }

    public void StartGame() {
        SceneManager.LoadScene("Stage2");
    }

    public void ShowMainMenu() {
        _mainMenu.SetActive(true);
        _optionsMenu.SetActive(false);
    }

    public void ShowOptionsMenu() {
        _mainMenu.SetActive(false);
        _optionsMenu.SetActive(true);
    }

    public void ShowCredits() {

    }
}
