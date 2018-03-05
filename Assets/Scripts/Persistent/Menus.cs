using UnityEngine;

public class Menus : MonoBehaviour {
    private static Menus _instance;

    public static Menus Instance { get { return _instance; } }

    private string Name { get { return "Menus"; } }

    private GameObject _pause;

    public GameObject Pause {
        get { return _pause; }
    }

    private GameObject _settings;

    public GameObject Settings {
        get { return _settings; }
    }

    private GameObject _gameOver;

    public GameObject GameOver {
        get { return _gameOver; }
    }

    // Use this for initialization
    private void Awake() {
        if (Instance == null) {
            _instance = this;

        } else if (Instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        Instance.name = Name;

        Initialize();
    }

    public void Revive() {
        Instance.GameOver.SetActive(false);
    }

    private void Initialize() {
        _pause = transform.Find("Pause").gameObject;
        _settings = transform.Find("Settings").gameObject;
        _gameOver = transform.Find("GameOver").gameObject;
    }
}

