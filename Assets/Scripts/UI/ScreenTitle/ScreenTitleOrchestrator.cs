using UnityEngine;

public class ScreenTitleOrchestrator : MonoBehaviour {    
    private GameObject _backgroundMusic;
    private GameObject _mainMenu, _settingsMenu;

    private void Start() {
        InitializeComponents();
        InitializePreferences();
    }

    private void InitializeComponents() {
        _mainMenu = GameObject.Find("MainMenu");
        _mainMenu.SetActive(false);

        _backgroundMusic = GameObject.Find("BackgroundMusic");
        _backgroundMusic.SetActive(false);

        _settingsMenu = GameObject.Find("SettingsMenu");
        _settingsMenu.SetActive(false);
    }
    
    private void InitializePreferences() {
        var volumeSaved = PlayerPrefs.GetFloat("MasterVolume", 1f);
        var audio = _backgroundMusic.GetComponent<AudioSource>();
        audio.volume = volumeSaved;
    }

    public GameObject GetMainMenu() {
        return _mainMenu;
    }

    public GameObject GetSettingsMenu() {
        return _settingsMenu;
    }
}
