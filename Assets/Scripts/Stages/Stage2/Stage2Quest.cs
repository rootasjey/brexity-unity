using UnityEngine;
using UnityEngine.UI;

public class Stage2Quest : MonoBehaviour {
    public float timeLeft;
    public GameObject GamePlay;

    private Text _questTimer;

	// Use this for initialization
	void Start () {
        _questTimer = transform.Find("QuestTimer").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!GamePlay.activeSelf) return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0) {
            var playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
            playerStats.Kill();
        }

        _questTimer.text = ((int)timeLeft).ToString();
    }
}
