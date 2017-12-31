using UnityEngine;

public class CameraTracking : MonoBehaviour {
    public GameObject player;
    private Vector3 _offset;

	// Use this for initialization
	void Start () {
        _offset = this.transform.position - player.transform.position;
	}
	
    // Called after every game object has been processed
    private void LateUpdate() {
        this.transform.position = player.transform.position + _offset;
    }
}
