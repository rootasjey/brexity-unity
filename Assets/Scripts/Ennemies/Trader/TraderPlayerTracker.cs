using System.Collections;
using UnityEngine;

public class TraderPlayerTracker : MonoBehaviour {

    public Transform player;
    public float timeBeforeTrigger;
    public bool isActive;

    private float _initialTimeBeforeTrigger;
    private Vector3 _playerLastPosition;
    private Rigidbody2D _traderRigidBody;

    private bool _resetingState = false;

	void Start () {
        _initialTimeBeforeTrigger = timeBeforeTrigger;

        _traderRigidBody = transform.GetComponent<Rigidbody2D>();
        _traderRigidBody.gravityScale = 0;
	}
	
	void Update () {
        if (!isActive) return;

        if (_traderRigidBody.gravityScale == 1) { // already falling
            var animator = GetComponent<Animator>();
            var traderIsOnFloor = animator.GetBool("Ground_Touch");
            

            if (traderIsOnFloor && !_resetingState) {
                _resetingState = true;

                var coroutine = ResetTraderProperties();
                StartCoroutine(coroutine);
            }

            return;
        }

        _resetingState = false;

        // Countdown
        timeBeforeTrigger -= Time.deltaTime;

        var playerCurrentPosition = player.position;
        var positionResult = _playerLastPosition - playerCurrentPosition;

        // Reset time if the player moved
        if (positionResult != Vector3.zero) {
            timeBeforeTrigger = _initialTimeBeforeTrigger;
        }

        _playerLastPosition = playerCurrentPosition;

        // If the countdown reaches 0, let the body down
        // Set the player life to 0 => Game over
        if (timeBeforeTrigger < 0) {
            transform.position = new Vector3(playerCurrentPosition.x, transform.position.y);
            _traderRigidBody.gravityScale = 1;
        }
    }

    IEnumerator ResetTraderProperties() {
        yield return new WaitForSeconds(5f);
        _traderRigidBody.gravityScale = 0;
        transform.position = new Vector3(transform.position.x, 15);

        var animator = GetComponent<Animator>();
        animator.Rebind();
    }
}
