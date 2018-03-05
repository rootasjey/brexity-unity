using UnityEngine;
using UnityEngine.UI;

public class ActionsPauseSettings : MonoBehaviour {
    private Stage2Orchestrator _orchestrator;
    private MenuNavigation _menuNavigation;
    private Slider _audioSlider;
    private AudioSource _backgroundMusic { get; set; }
    private AudioSource _ambianceSound { get; set; }

    // Use this for initialization
    void Start() {
        InitializeComponents();
    }

    private void InitializeComponents() {
        _orchestrator = GameObject.Find("Orchestrator")
                        .GetComponent<Stage2Orchestrator>();

        _menuNavigation = GetComponent<MenuNavigation>();
        _audioSlider = GameObject.Find("ButtonAudio")
                        .GetComponentInChildren<Slider>();

        _backgroundMusic = GameObject.Find("BackgroundMusic")
                            .GetComponent<AudioSource>();

        if (GameObject.Find("AmbianceSound")) {
            _ambianceSound = GameObject.Find("AmbianceSound")
                .GetComponent<AudioSource>();
        }
        

        _audioSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
    }

    private void Update() {
        if (!_orchestrator.SettingsMenu.activeSelf) return;

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
        Menus.Instance.Settings.SetActive(false);
        Menus.Instance.Pause.SetActive(false);

        SaveSettings();
    }

    public void ChangeMasterVolume() {
        if (_backgroundMusic) _backgroundMusic.volume = _audioSlider.value;
        if (_ambianceSound) _ambianceSound.volume = _audioSlider.value;
    }

    private void SaveSettings() {
        PlayerPrefs.SetFloat("MasterVolume", _audioSlider.value);
        PlayerPrefs.Save();

        _orchestrator.SetLastSavedVolume(_audioSlider.value);
    }
}
