using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActionButtonsMainMenu : MonoBehaviour {
    private GameObject _mainMenu, _settingsMenu;
    private MenuNavigation _menuNavigation;
    private Slider _audioSlider;

    private void Start() {
        _mainMenu = GameObject.Find("MainMenu");
        _settingsMenu = GameObject.Find("SettingsMenu");
    }
    
    // Use this for initialization
    // BUTTONS CLICK CALLBACKS
    // -----------------------
    public void StartGame() {
        SceneManager.LoadScene("Stage2_town");
    }

    public void GoToSettings() {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }

    public void ExitGame() {
        ExitGame();
    }
}
