using Assets.Scripts.Stages;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage2Quest : MonoBehaviour {
    public float timeLeft;
    public GameObject GamePlay;

    private Stage2Orchestrator _orchestrator;

    private float _animationDuration = 2f;
    private float _animationDurationLeft;
    private float _angleStep = 0.1f;

    private Quest _quest;

    public Quest Quest { get { return _quest; } }

    // Use this for initialization
    void Start () {
        _orchestrator = GameObject.Find("Orchestrator")
            .GetComponent<Stage2Orchestrator>();

        _animationDurationLeft = _animationDuration;

        InitQuest();
    }

    private void InitQuest() {
        _quest = new Quest {
            Objectives = new List<Objective> {
            new Objective() {
                Name = "Get rid of the tracker"
            },

            new Objective() {
                Name = "Find women’s sexy red lingerie"
            },

            new Objective() {
                Name = "Find a drone and Dr tooth"
            },

            new Objective() {
                Name = "Find batteries and people like yourself"
            },

            new Objective() {
                Name = "Find the surgeon"
            },

            new Objective() {
                Name = "Go watch the video tape at the depository"
            }
        }
        };
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

        var timerRect = _orchestrator.Timer.GetComponent<RectTransform>();

        timerRect
            .eulerAngles = new Vector3(
                timerRect.eulerAngles.x,
                timerRect.eulerAngles.y,
                timerRect.eulerAngles.z + _angleStep);

        _orchestrator.TimerText.text = ((int)timeLeft).ToString();
    }
}
