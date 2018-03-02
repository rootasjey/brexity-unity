using UnityEngine;

public class CameraTracking : MonoBehaviour {
    public float smoothTimeX;
    public float smoothTimeY;
    public float shakeTimer;
    public float shakeAmount;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    public Vector2 velocity;

    private Stage2Orchestrator _orchestrator;

    // Use this for initialization
    void Start () {
        _orchestrator = GameObject.Find("Orchestrator")
                        .GetComponent<Stage2Orchestrator>();
    }

    private void FixedUpdate() {
        if (_orchestrator == null) {
            _orchestrator = GameObject.Find("Orchestrator")
                        .GetComponent<Stage2Orchestrator>();
        }

        if (_orchestrator.Player == null || 
            !_orchestrator.Player.activeSelf) {

            return;
        }

        if (minCameraPos == Vector3.zero) {
            //_minCameraPos = new Vector3(-2.7f, 2.9f);
            minCameraPos = _orchestrator.MinScreenSpaceLimit;
        }

        if (maxCameraPos == Vector3.zero) {
            maxCameraPos = _orchestrator.MaxScreenSpaceLimit;
            //_maxCameraPos = new Vector3(
            //    _orchestrator.RightLimitBorder.transform.position.x - Camera.main.orthographicSize,
            //    _orchestrator.TopLimitBorder.transform.position.y - Camera.main.orthographicSize);
        }

        float posX = Mathf.SmoothDamp(transform.position.x, 
            _orchestrator.Player.transform.position.x, ref velocity.x, smoothTimeX);

        float posY = Mathf.SmoothDamp(transform.position.y, 
            _orchestrator.Player.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(
                Mathf.Clamp(posX, minCameraPos.x, maxCameraPos.x),
                Mathf.Clamp(posY, minCameraPos.y, maxCameraPos.y),
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
