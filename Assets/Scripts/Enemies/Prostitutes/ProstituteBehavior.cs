using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProstituteBehavior : MonoBehaviour {
    public float timeBeforeTurnOff;
    public float offDuration;

    private float _initialTimeBeforeTurnOff;
    private float _initialOffDuration;

    private Sprite _defaultSprite;

    private Animator _animator;

    private GameObject _spriteGameObject;
    private SpriteRenderer _spriteRenderer;

    private Stage2Orchestrator _orchestrator;

    private BoxCollider2D _colliderDetection;

    // Use this for initialization
    void Start () {
        _orchestrator = GameObject.Find("Orchestrator").GetComponent<Stage2Orchestrator>();

        _initialTimeBeforeTurnOff = timeBeforeTurnOff;
        _initialOffDuration = offDuration;

        _spriteGameObject = transform.Find("Sprite").gameObject;
        _animator = _spriteGameObject.GetComponent<Animator>();

        _spriteRenderer = _spriteGameObject.GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteRenderer.sprite;

        _colliderDetection = _spriteGameObject.transform.Find("AlertDetection")
                .gameObject.GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        timeBeforeTurnOff -= Time.deltaTime;

        if (timeBeforeTurnOff <= 0) {
            _spriteRenderer.sprite = _defaultSprite;
            _spriteRenderer.color = new Color(0, 0, 0);
            _animator.enabled = false;

            _spriteGameObject.transform.Find("AlertDetection")
                .gameObject.SetActive(false);

            offDuration -= Time.deltaTime;

        }

        if (offDuration <= 0) {
            _animator.enabled = true;
            _spriteRenderer.color = new Color(1, 1, 1);

            offDuration = _initialOffDuration;
            timeBeforeTurnOff = _initialTimeBeforeTurnOff;
        }
    }
}
