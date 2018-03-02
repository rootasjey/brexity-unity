using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentQuest : MonoBehaviour {

    public static PersistentQuest instance = null;

    public GameObject QuestPrefab;

    // Use this for initialization
    private void Awake() {
        if (instance == null) {
            instance = this;

        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        //InitGame();
        instance.name = "Quest";
    }

    public void ResetTimer() {
        instance.GetComponent<Stage2Quest>().ResetTime();
    }

    private void InitGame() {
        var _ui = Instantiate(QuestPrefab);
        _ui.name = "Quest";
    }
}
