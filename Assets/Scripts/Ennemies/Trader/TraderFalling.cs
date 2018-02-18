using UnityEngine;

public class TraderFalling : MonoBehaviour {
    public LayerMask Ground;

    private void FixedUpdate() {
        var animator = GetComponent<Animator>();

        var raycast = Physics2D.Raycast(
            new Vector2(transform.position.x, 
            transform.position.y), Vector2.down,
            5,
            Ground);

        //Debug.DrawRay(transform.position, new Vector2(0,-5), Color.red);

        if (raycast.collider != null) {
            var range = new Vector2(transform.position.x, transform.position.y) - raycast.point;

            if (range.y < 2.5) {
                animator.SetBool("Ground_Touch", true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
    }
}
