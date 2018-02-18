using UnityEngine;

public class ScreenTitleOrchestrator : MonoBehaviour {    
    private GameObject _backgroundMusic;
    private GameObject _mainMenu, _settingsMenu;

    private void Start() {
        InitializeComponents();
    }

    private void InitializeComponents() {
        _mainMenu = GameObject.Find("MainMenu");
        _mainMenu.SetActive(false);

        _backgroundMusic = GameObject.Find("BackgroundMusic");
        _backgroundMusic.SetActive(false);

        _settingsMenu = GameObject.Find("SettingsMenu");
        _settingsMenu.SetActive(false);
    }
    

    public GameObject GetMainMenu() {
        return _mainMenu;
    }

    public GameObject GetSettingsMenu() {
        return _settingsMenu;
    }
}
