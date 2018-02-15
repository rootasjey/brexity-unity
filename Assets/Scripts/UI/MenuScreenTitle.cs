using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreenTitle : MonoBehaviour {
    private void OnGUI() {
        GUI.Label(new Rect(10, 10, 100, 100), "START GAME");
    }
}
