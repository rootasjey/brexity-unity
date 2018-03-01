using UnityEngine;
using UnityEngine.UI;

public class Stage2Orchestrator : MonoBehaviour {

    #region properties

    private GameObject _pauseMenu;

    public GameObject PauseMenu {
        get {
            if (_pauseMenu == null) {
                _pauseMenu = GameObject.Find("PauseMenu");
            }

            return _pauseMenu;
        }
    }

    private GameObject _settingsMenu;

    public GameObject SettingsMenu {
        get {
            if (_settingsMenu == null) {
                _settingsMenu = GameObject.Find("SettingsMenu");
            }

            return _settingsMenu;
        }
    }

    private GameObject _inventory;

    public GameObject Inventory {
        get {
            if (_inventory == null) {
                _inventory = GameObject.Find("Inventory");
            }
            return _inventory;
        }
    }

    private GameObject _audio;

    public GameObject Audio {
        get {
            if (_audio == null) {
                _audio = GameObject.Find("Audio");
            }
            return _audio;
        }
    }

    private AudioSource _backgroundMusic;

    public AudioSource BackgroundMusic {
        get {
            if (_backgroundMusic == null) {
                _backgroundMusic = GameObject.Find("BackgroundMusic")
                            .GetComponent<AudioSource>();
            }
            return _backgroundMusic;
        }
    }

    private PlayerStats _playerStats;

    public PlayerStats PlayerStats {
        get {
            if (_playerStats == null) {
                _playerStats = Player ? Player.GetComponent<PlayerStats>() : null;
            }
            return _playerStats;
        }
    }

    private GameObject _player;

    public GameObject Player {
        get {
            if (_player == null) {
                _player = GameObject.Find("Player");
            }
            return _player;
        }
    }

    private Vector2 _minScreenSpaceLimit;

    public Vector2 MinScreenSpaceLimit {
        get {
            if (_minScreenSpaceLimit == Vector2.zero) {
                var firstMap = GameObject.Find("FirstMap");
                if (firstMap != null) {
                    var rect = firstMap.GetComponent<RectTransform>();

                    _minScreenSpaceLimit = new Vector2(
                        rect.position.x,
                        rect.position.y - 0.5f
                        );
                }
                //_minScreenSpaceLimit = new Vector3(38.4f, 2.9f);
            }
            return _minScreenSpaceLimit;
        }
    }

    private Vector2 _maxScreenSpaceLimit;

    public Vector2 MaxScreenSpaceLimit {
        get {
            if (_maxScreenSpaceLimit == Vector2.zero) {
                //var lastMap = GameObject.Find("LastMap");

                //if (lastMap != null) {
                //    var rect = lastMap.GetComponent<RectTransform>();

                //    _maxScreenSpaceLimit = new Vector2(
                //        rect.position.x,
                //        rect.position.y + rect.sizeDelta.y
                //        );
                //}
                _maxScreenSpaceLimit = new Vector3(229, 12);
            }
            return _maxScreenSpaceLimit; }
    }

    private GameObject _deathScreen;

    public GameObject DeathScreen {
        get {
            if (_deathScreen == null) {
                _deathScreen = GameObject.Find("DeathScreen");
            }
            return _deathScreen;
        }
    }

    private GameObject _gamePlay;

    public GameObject GamePlay {
        get {
            if (_gamePlay == null) {
                _gamePlay = GameObject.Find("GamePlay");
            }
            return _gamePlay;
        }
    }

    private GameObject _timer;

    public GameObject Timer {
        get {
            if (_timer == null) {
                _timer = GameObject.Find("Timer");
            }
            return _timer;
        }
    }

    private GameObject _timerPopup;

    public GameObject TimerPopup {
        get {
            if (_timerPopup == null) {
                _timerPopup = GameObject.Find("TimerPopup");
            }
            return _timerPopup;
        }
    }

    private Text _timerText;

    public Text TimerText {
        get {
            if (_timerText == null) {
                _timerText = TimerPopup.transform.Find("TimerText")
                                .gameObject.GetComponent<Text>();
            }
            return _timerText;
        }
    }


    private float _lastVolumeValue { get; set; }

    #endregion properties

    private void Start() {
        InitializePreferences();
        InitializeComponents();

        Time.timeScale = 1;
    }

    private void Update() {
        if (PlayerStats && PlayerStats.IsDead()) {
            DeathScreen.SetActive(true);
            Time.timeScale = 0;
            return;
        }

        if (Input.GetButtonDown("Pause") || Input.GetKeyDown(KeyCode.Escape)) {
            if (PauseMenu.activeSelf) {
                ResumeGame();

            } else {
                PauseMenu.SetActive(true);
                Time.timeScale = 0;

                _lastVolumeValue = _backgroundMusic.volume;
                BackgroundMusic.volume = 0;
                Audio.SetActive(false);
            }
        }

        if (!PauseMenu.activeSelf && Input.GetKeyDown(KeyCode.I)) {
            Inventory.SetActive(!Inventory.activeSelf);
        }
    }

    private void InitializeComponents() {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        DeathScreen.SetActive(false);
        Inventory.SetActive(false);
        TimerPopup.SetActive(false);

        GamePlay.SetActive(false);

        Audio.SetActive(false);
    }

    private void InitializePreferences() {
        var volumeSaved = PlayerPrefs.GetFloat("MasterVolume", 1f);
        BackgroundMusic.volume = volumeSaved;
    }

    public void ResumeGame() {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;

        BackgroundMusic.volume = _lastVolumeValue;
        Audio.SetActive(true);
    }

    public void SetLastSavedVolume(float value) {
        _lastVolumeValue = value;
    }

    public float GetCurrentMusicVolume() {
        return 0f;
    }
}
