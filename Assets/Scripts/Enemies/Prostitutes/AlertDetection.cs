using UnityEngine;

public class AlertDetection : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        var playerStats = collision.gameObject.GetComponent<PlayerStats>();

        if (!playerStats) return;

        playerStats.Kill();
    }
}
