using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
    public float parallaxSpeed;

    private Transform _cameraTransform;
    private float _lastCameraX;

	// Use this for initialization
	void Start () {
        _cameraTransform = Camera.main.transform;
        _lastCameraX = _cameraTransform.transform.position.x;
    }
	
	// Update is called once per frame
	void Update () {
        float deltaX = _cameraTransform.position.x - _lastCameraX;
        transform.position += Vector3.right * (deltaX * parallaxSpeed);

		_lastCameraX = _cameraTransform.transform.position.x;
    }
}
