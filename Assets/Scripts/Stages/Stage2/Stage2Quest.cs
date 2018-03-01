using UnityEngine;
using UnityEngine.UI;

public class Stage2Quest : MonoBehaviour {
    public float timeLeft;
    public GameObject GamePlay;

    private Stage2Orchestrator _orchestrator;

    private float _animationDuration = 2f;
    private float _animationDurationLeft;
    private float _angleStep = 0.1f;

    private RectTransform _timerRect;

    // Use this for initialization
    void Start () {
        _orchestrator = GameObject.Find("Orchestrator").GetComponent<Stage2Orchestrator>();
        _timerRect = _orchestrator.Timer.GetComponent<RectTransform>();
        _animationDurationLeft = _animationDuration;
    }
	
	// Update is called once per frame
	void Update () {
        if (!_orchestrator.GamePlay.activeSelf) return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0) {
            _orchestrator.PlayerStats.Kill();
        }

        _animationDurationLeft -= Time.deltaTime;

        if (_animationDurationLeft <= 0) {
            _animationDurationLeft = _animationDuration * 2;
            _angleStep = -_angleStep;
        }

        _timerRect.eulerAngles = new Vector3(
                _timerRect.eulerAngles.x,
                _timerRect.eulerAngles.y,
                _timerRect.eulerAngles.z + _angleStep);

        _orchestrator.TimerText.text = ((int)timeLeft).ToString();
    }
}
