using System.Collections;
using UnityEngine;

public class TraderFalling : MonoBehaviour {
    Animator _animator;

    public AudioClip[] reactionVoices;

    private void Start() {
        _animator = GetComponent<Animator>();

        Physics2D.IgnoreLayerCollision(13, 14);
        Physics2D.IgnoreLayerCollision(11, 14);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (_animator.GetBool("Ground_Touch") == true) return;

        _animator.SetBool("Ground_Touch", true);

        var cameraTracking = Camera.main.GetComponent<CameraTracking>();
        cameraTracking.Shake(.1f, .3f);

        StartCoroutine(PlayReactionVoice());

        StartCoroutine(KillTrader());

        var playerStats = collision.gameObject.GetComponent<PlayerStats>();
        if (!playerStats) return;

        playerStats.Kill();
    }

    private IEnumerator PlayReactionVoice() {
        yield return new WaitForSeconds(2);

        var clip = reactionVoices[Random.Range(0, reactionVoices.Length)];
        var audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.Play();
    }

    private IEnumerator KillTrader() {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
