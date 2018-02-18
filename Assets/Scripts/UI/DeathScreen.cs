using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour {
    private PlayerStats _playerStats;

	// Use this for initialization
	void Start () {
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (_playerStats.IsDead()) {
            gameObject.SetActive(true);
            ChooseRandomTitle();
            Time.timeScale = 0;
        }
	}

    private void ChooseRandomTitle() {
        var titles = new List<string>() {
            "RUN'S OVER",
            "YOU DIDN'T MAKE IT",
            "TIME TO DIE",
            "YOU ARE TERMINATED"
        };

        var randomNumber = new System.Random().Next(0, titles.Count);
            
        var title = transform.Find("Title");
        var textTitle = title.GetComponent<Text>();
        textTitle.text = titles[randomNumber];
    }
}
