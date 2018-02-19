using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour {
	void Start () {
        ChooseRandomTitle();
    }

    private void ChooseRandomTitle() {
        var titles = new List<string>() {
            "RUN'S OVER",
            "YOU DIDN'T MAKE IT",
            "TIME TO DIE",
            "YOU ARE TERMINATED"
        };

        var randomNumber = new System.Random().Next(0, titles.Count);

        var buttons = transform.Find("Buttons");
        var title = buttons.Find("Title");

        var textTitle = title.gameObject.GetComponent<Text>();
        textTitle.text = titles[randomNumber];
    }
}
