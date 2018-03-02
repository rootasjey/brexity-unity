using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentHUD : MonoBehaviour {
    public static PersistentHUD instance = null;

    public GameObject HUDPrefab;

    // Use this for initialization
    private void Awake() {
        if (instance == null) {
            instance = this;

        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        //InitGame();
        instance.name = "HUD";
    }

    private void InitGame() {
        var _hud = Instantiate(HUDPrefab);
        _hud.name = "HUD";
    }
}
