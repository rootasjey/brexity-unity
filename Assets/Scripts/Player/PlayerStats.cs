using Assets.Scripts.Stages;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoBehaviour {
    public float Life;
    [Range(0, 100)]
    public float stealth;

    private KeyValuePair<int, GameObject> questIndexGameObject;

    public KeyValuePair<int, GameObject> QuestIndexGameObject
    {
        get
        {
            return questIndexGameObject;
        }

        set
        {
            questIndexGameObject = value;
        }
    }

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
