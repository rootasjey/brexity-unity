using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public float Life;
    [Range(0, 100)]
    public float stealth;

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
