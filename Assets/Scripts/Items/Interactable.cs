using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {
    public Font customFont;

    public string key;
    public float yOffset;

    public Sprite hintBackground;

    private GameObject _hintInteractable;

    private Text _textComponent;
    private bool _isPlayerInRange { get; set; }

    private float _initialTime = 1f;
    private float _timeLeft;
    private float _deltaY = 0.01f;
    
    private void Update() {
        if (!_isPlayerInRange) return;

        CheckInteractionInput();
        AnimateHint();
    }

    public virtual void CheckInteractionInput() {
        // NOTE: Use Settings.GetInteractionKey()
        if (_isPlayerInRange && Input.GetKeyDown(KeyCode.E)) {
            Interact();
        }
    }

    private void AnimateHint() {
        if (_timeLeft > 0) {
            _timeLeft -= Time.deltaTime;
            _hintInteractable.transform.position =
                _hintInteractable.transform.position - new Vector3(0, _deltaY);

        } else {
            _timeLeft = _initialTime;
            _deltaY = -_deltaY;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var playerStats = collision.gameObject.GetComponent<PlayerStats>();
        if (!playerStats) return;

        ShowPopupHint();

        _isPlayerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        var playerStats = collision.gameObject.GetComponent<PlayerStats>();
        if (!playerStats) return;

        HidePopupHint();

        _isPlayerInRange = false;
    }

    private void ShowPopupHint() {
        if (gameObject.GetComponent<Canvas>() == null) {
            gameObject.AddComponent<Canvas>();
        }

        if (_hintInteractable == null) {
            _hintInteractable = new GameObject("HintInteractable");
            _hintInteractable.transform.parent = gameObject.transform;
            _hintInteractable.transform.localScale = new Vector3(.6f, .6f, .6f);

            // image
            var spriteGameObject = new GameObject("TextHintBackground");
            spriteGameObject.transform.parent = _hintInteractable.transform;
            spriteGameObject.transform.localPosition = Vector3.zero;

            var spriteRenderer = spriteGameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = hintBackground;
            spriteRenderer.transform.localScale = new Vector3(2, 2, 2);

            // text
            var textGameObject = new GameObject("TextHint");
            textGameObject.transform.parent = _hintInteractable.transform;
            textGameObject.transform.localPosition = new Vector3(0, -0.05f, 0);

            _textComponent = textGameObject.gameObject.AddComponent<Text>();

            _textComponent.font = customFont;
            _textComponent.fontSize = 30;
            _textComponent.alignment = TextAnchor.MiddleCenter;
            _textComponent.color = new Color(186.0f/ 255.0f, 42.0f / 255.0f, 186.0f / 255.0f, 1);

            _textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
            _textComponent.verticalOverflow = VerticalWrapMode.Overflow;
            _textComponent.transform.localScale = new Vector3(.02f, .02f, .02f);
        }

        var verticalOffset = yOffset + 2.5f;
        _hintInteractable.transform.localPosition = new Vector3(0, verticalOffset, 0);

        _textComponent.text = string.IsNullOrEmpty(key) ? "E": key; // update w/ player key binding
        _hintInteractable.SetActive(true);
    }

    private void HidePopupHint() {
        if (_hintInteractable == null) return;

        _hintInteractable.SetActive(false);
    }

    public virtual void Interact() {

    }
}
