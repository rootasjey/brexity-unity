using UnityEngine;

public class TraderFalling : MonoBehaviour {
    public LayerMask Ground;
    Animator _animator;

    private void Start() {
        _animator = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(13, 14);
    }

    //private void FixedUpdate() {
    //    var animator = GetComponent<Animator>();

    //    var raycast = Physics2D.Raycast(
    //        new Vector2(transform.position.x, 
    //        transform.position.y), Vector2.down,
    //        5,
    //        Ground);

    //    //Debug.DrawRay(transform.position, new Vector2(0,-5), Color.red);

    //    if (raycast.collider != null) {
    //        var range = new Vector2(transform.position.x, transform.position.y) - raycast.point;

    //        if (range.y < 2.5) {
    //            animator.SetBool("Ground_Touch", true);
    //        }
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision) {
        _animator.SetBool("Ground_Touch", true);

        var playerStats = collision.gameObject.GetComponent<PlayerStats>();
        if (!playerStats) return;

        playerStats.Kill();
    }
}
