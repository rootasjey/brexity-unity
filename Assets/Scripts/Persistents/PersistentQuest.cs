using UnityEngine;
using UnityEngine.UI;

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

    //private void InitGame() {
    //}

    public void ResetTimer() {
        instance.GetComponent<Stage2Quest>().ResetTime();
    }
}
