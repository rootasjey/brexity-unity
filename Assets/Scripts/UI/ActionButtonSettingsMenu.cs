using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonSettingsMenu : MonoBehaviour {
    private GameObject _mainMenu, _settingsMenu;
    private MenuNavigation _menuNavigation;
    private Slider _audioSlider;
    private AudioSource _backgroundMusic;

    // Use this for initialization
    void Start () {
        InitializeComponents();
    }

    private void InitializeComponents() {
        _mainMenu = GameObject.Find("MainMenu");
        _settingsMenu = GameObject.Find("SettingsMenu");

        _menuNavigation = GetComponent<MenuNavigation>();
        _audioSlider = GameObject.Find("ButtonAudio")
                        .GetComponentInChildren<Slider>();

        _backgroundMusic = GameObject.Find("BackgroundMusic")
                            .GetComponent<AudioSource>();
    }

    private void Update() {
        if (!_settingsMenu.activeSelf) return;

        if (_menuNavigation.GetCursorIndex() == 0) {
            _audioSlider.interactable = true;

            if (Input.GetKey(KeyCode.LeftArrow)) {
                _audioSlider.value -= 0.01f;
            }

            if (Input.GetKey(KeyCode.RightArrow)) {
                _audioSlider.value += 0.01f;
            }

        } else { _audioSlider.interactable = false; }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            BackToMainMenu();
        }
    }

    public void BackToMainMenu() {
        _settingsMenu.SetActive(false);
        _mainMenu.SetActive(true);

        SaveSettings();
    }

    public void ChangeMasterVolume() {
        _backgroundMusic.volume = _audioSlider.value;
    }

    private void SaveSettings() {

    }
}
