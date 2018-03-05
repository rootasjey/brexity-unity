using Assets.Scripts.Stages;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage2Quest : MonoBehaviour {
    public float timeLeft;
    public GameObject GamePlay;

    private Stage2Orchestrator _orchestrator;

    private float _animationDuration = 1f;
    private float _animationDurationLeft;
    private float _scaleStep = -0.0025f;

    private Quest _quest;

    public Quest Quest { get { return _quest; } }

    // Use this for initialization
    void Start () {
        _orchestrator = GameObject.Find("Orchestrator")
            .GetComponent<Stage2Orchestrator>();

        _animationDurationLeft = _animationDuration;

        InitQuest();

        if (_orchestrator.TimerSandGlass) {
            _orchestrator.TimerSandGlass
           .GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }
       

    public void ResetQuest()
    {
        _quest.CurrentStep = 0;
        InitQuest();

        _orchestrator.TimerSandGlass
           .GetComponent<RectTransform>().localScale = Vector3.one;
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
	
    public void ResetTime() {
        timeLeft = 99f;
    }

    // Update is called once per frame
    void Update () {
        if (_orchestrator == null) {
            _orchestrator = GameObject.Find("Orchestrator")
            .GetComponent<Stage2Orchestrator>();
        }

        if (_orchestrator == null || _orchestrator.GamePlay == null) return;
        if (!_orchestrator.GamePlay.activeSelf) return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0) {
            _orchestrator.PlayerStats.Kill();
        }

        _animationDurationLeft -= Time.deltaTime;

        if (_animationDurationLeft <= 0) {
            _animationDurationLeft = _animationDuration;
            _scaleStep = -_scaleStep;
        }

        var timerRect = _orchestrator.TimerSandGlass.GetComponent<RectTransform>();

        timerRect.localScale = new Vector3(
            timerRect.localScale.x + _scaleStep,
            timerRect.localScale.y + _scaleStep,
            timerRect.localScale.z + _scaleStep);

        //timerRect
        //    .eulerAngles = new Vector3(
        //        timerRect.eulerAngles.x,
        //        timerRect.eulerAngles.y,
        //        timerRect.eulerAngles.z + _angleStep);

        _orchestrator.TimerText.text = ((int)timeLeft).ToString();
    }
}
