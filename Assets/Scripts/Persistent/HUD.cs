using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    /* PRIVATE PROPS */
    private static HUD _instance;

    /* PUBLIC PROPS */
    public static HUD Instance { get { return _instance; } }

    private string Name { get { return "HUD"; } }

    private Text _textTimer;

    public Text TextTimer {
        get { return _textTimer; }
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

    private void Initialize() {
        _textTimer = transform
            .Find("Timer")
            .Find("TimerPopup")
            .Find("TimerText").GetComponent<Text>();
    }
}
