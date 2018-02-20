using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public float Life;

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
