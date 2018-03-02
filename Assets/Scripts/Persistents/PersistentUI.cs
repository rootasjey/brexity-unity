using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentUI : MonoBehaviour {

    public static PersistentUI instance = null;

    public GameObject HUDPrefab;

    private GameObject _pauseMenu;

    public GameObject PauseMenu {
        get { return _pauseMenu; }
    }

    private GameObject _settingsMenu;

    public GameObject SettingsMenu {
        get { return _settingsMenu; }
    }

    private GameObject _deathScreen;

    public GameObject DeathScreen {
        get { return _deathScreen; }
    }

    // Use this for initialization
    private void Awake() {
        if (instance == null) {
            instance = this;

        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        instance.name = "UI";

        InitGame();
    }

    public void Revive() {
        instance.transform
            .Find("DeathScreen")
            .gameObject.SetActive(false);
    }

    private void InitGame() {
        _pauseMenu = transform.Find("PauseMenu").gameObject;
        _settingsMenu = transform.Find("SettingsMenu").gameObject;
        _deathScreen = transform.Find("DeathScreen").gameObject;
    }
}

