using UnityEngine;
using UnityEngine.UI;

public class PersistentQuest : MonoBehaviour {

    public static PersistentQuest instance = null;

    public GameObject QuestPrefab;

    private Text _timerText;

    public Text TimerText {
        get { return _timerText; }
    }

    // Use this for initialization
    private void Awake() {
        if (instance == null) {
            instance = this;

        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        InitGame();
        instance.name = "Quest";
    }

    private void InitGame() {
        _timerText = transform.Find("QuestTimer")
                      .gameObject.GetComponent<Text>();
    }

    public void ResetTimer() {
        instance.GetComponent<Stage2Quest>().ResetTime();
    }
}
