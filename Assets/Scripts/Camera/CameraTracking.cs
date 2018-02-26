using UnityEngine;

public class CameraTracking : MonoBehaviour {
    public GameObject player;

    public float smoothTimeX;
    public float smoothTimeY;
    public float shakeTimer;
    public float shakeAmount;

    private Vector3 _minCameraPos;
    private Vector3 _maxCameraPos;

    public Vector2 velocity;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");

        var leftLimitBorder = GameObject.Find("LeftLimitBorder").transform.position;
        var rightLimitBorder = GameObject.Find("RightLimitBorder").transform.position;
        var topLimitBorder = GameObject.Find("TopLimitBorder").transform.position;
        var bottomLimitBorder = GameObject.Find("BottomLimitBorder").transform.position;

        var cameraSize = (Camera.main.orthographicSize);

        _minCameraPos = new Vector3(leftLimitBorder.x, bottomLimitBorder.y + cameraSize);
        _maxCameraPos = new Vector3(rightLimitBorder.x - cameraSize, topLimitBorder.y - cameraSize);
    }

    private void FixedUpdate() {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(
                Mathf.Clamp(posX, _minCameraPos.x, _maxCameraPos.x),
                Mathf.Clamp(posY, _minCameraPos.y, _maxCameraPos.y),
                Mathf.Clamp(transform.position.z, transform.position.z, transform.position.z));
    }

    private void Update() {
        if (shakeTimer >= 0) {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmount;

            transform.position = new Vector3(
                transform.position.x + shakePos.x, 
                transform.position.y + shakePos.y, 
                transform.position.z);

            shakeTimer -= Time.deltaTime;
        }
    }

    public void Shake(float shakePower, float shakeDuration) {
        shakeAmount = shakePower;
        shakeTimer = shakeDuration;
    }
}
