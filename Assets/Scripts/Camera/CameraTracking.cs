using UnityEngine;

public class CameraTracking : MonoBehaviour {
    public GameObject player;
    private Vector3 _offset;

    public float offsetX = 150;

    public double LeftBorder = 0;
    public double RightBorder = 0;

    private Rigidbody2D _playerRB;

    // Use this for initialization
    void Start () {
        _offset = this.transform.position - player.transform.position;
        _playerRB = player.GetComponent<Rigidbody2D>();
    }
	
    // Called after every game object has been processed
    private void LateUpdate() {
        // Check world borders
        var playerPosition = player.transform.position;

        if (playerPosition.x <= LeftBorder || playerPosition.x >= RightBorder) {
            return;
        }

        // backup
        this.transform.position = player.transform.position + _offset;

        //Vector3 velocity = Vector3.zero;

        //float time = 7f / Mathf.Abs(_playerRB.velocity.x + 1);

        //if (_playerRB.velocity.x >= 0 && playerPosition.x - this.transform.position.x >= 3) {
        //    var newPosition = this.transform.position + new Vector3(offsetX, 0);

        //    this.transform.position = Vector3.SmoothDamp(this.transform.position, newPosition, ref velocity, time);

        //} else if (_playerRB.velocity.x < 0 && playerPosition.x - this.transform.position.x <= 1) {
        //    var newPosition = this.transform.position - new Vector3(offsetX, 0);

        //    this.transform.position = Vector3.SmoothDamp(this.transform.position, newPosition, ref velocity, time);
        //}
    }
}
