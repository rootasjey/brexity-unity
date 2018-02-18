using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public float Life;

	// Use this for initialization
	//void Start () {
		
	//}
	
	//// Update is called once per frame
	//void Update () {
		
	//}

    public float GetLife() {
        return Life;
    }

    public void Kill() {
        Life = 0;
    }

    public bool IsDead() {
        return Life <= 0;
    }
}
