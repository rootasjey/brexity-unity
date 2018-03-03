using UnityEngine;
using UnityEngine.UI;

public class ActionsScreenTitleSettings : MonoBehaviour {
    private MenuNavigation _menuNavigation;
    private Slider _audioSlider;
    private AudioSource _backgroundMusic;
    private AudioSource _ambianceSound { get; set; }

    private ScreenTitleOrchestrator _orchestrator;

    // Use this for initialization
    void Start () {
        InitializeComponents();
    }

    private void InitializeComponents() {
        _orchestrator = GameObject.Find("Orchestrator")
                        .GetComponent<ScreenTitleOrchestrator>();

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
        if (!_orchestrator.GetSettingsMenu().activeSelf) return;

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
        _orchestrator.GetSettingsMenu().SetActive(false);
        _orchestrator.GetMainMenu().SetActive(true);

        SaveSettings();
    }

    public void ChangeMasterVolume() {
        if (_backgroundMusic) _backgroundMusic.volume = _audioSlider.value;
        if (_ambianceSound) _ambianceSound.volume = _audioSlider.value;
    }

    private void SaveSettings() {
        PlayerPrefs.SetFloat("MasterVolume", _audioSlider.value);
        PlayerPrefs.Save();
    }
}
