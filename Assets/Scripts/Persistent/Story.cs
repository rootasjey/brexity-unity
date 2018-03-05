using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Hero's story.
/// </summary>
public class Story : MonoBehaviour {
    /* PRIVATE PROPS */
    private static Story _instance;

    /* PUBLIC PROPS */
    public static Story Instance { get { return _instance; } }

    private string Name { get { return "Story"; } }

    // Use this for initialization
    private void Awake() {
        if (Instance == null) {
            _instance = this;

        } else if (Instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        Instance.name = Name;
    }

    public void ResetTimer() {
        Instance.GetComponent<Stage2Quest>().ResetTime();
    }
}
