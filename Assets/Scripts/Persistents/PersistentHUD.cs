using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersistentHUD : MonoBehaviour {
    public static PersistentHUD instance = null;

    public GameObject HUDPrefab;

    private Text _textTimer;

    public Text TextTimer {
        get { return _textTimer; }
    }


    // Use this for initialization
    private void Awake() {
        if (instance == null) {
            instance = this;

        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        instance.name = "HUD";
        InitGame();
    }

    private void InitGame() {
        _textTimer = transform
            .Find("Timer")
            .Find("TimerPopup")
            .Find("TimerText").GetComponent<Text>();
    }
}
