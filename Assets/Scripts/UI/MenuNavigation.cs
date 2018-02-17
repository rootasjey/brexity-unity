using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour {
    private List<Button> _buttonsList;

    private int _buttonsCount = 0;
    private int _cursorIndex = -1;

    private Color _initialColor;

	void Start () {
        _buttonsList = new List<Button>(gameObject.GetComponentsInChildren<Button>());
        _buttonsCount = _buttonsList.Count;

        AddHoverOnButtons();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            _cursorIndex--;
            if (_cursorIndex < 0) _cursorIndex = _buttonsCount - 1;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            _cursorIndex++;
            _cursorIndex = _cursorIndex % _buttonsCount;
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            ValidateChoice();
        }
	}

    private void OnGUI() {
        SetInitialTextColor();

        if (_cursorIndex < 0 || _cursorIndex >= _buttonsCount) return;

        var buttonSelected = _buttonsList[_cursorIndex];
        if (buttonSelected == null) return;

        var textComponent = buttonSelected.gameObject.GetComponentInChildren<Text>();
        textComponent.color = Color.yellow;

        buttonSelected.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 0);
    }

    public int GetCursorIndex() {
        return _cursorIndex;
    }

    private void SetInitialTextColor() {
        foreach (var button in _buttonsList) {
            var textComponent = button.gameObject.GetComponentInChildren<Text>();
            textComponent.color = _initialColor;

            button.gameObject.transform.localScale = new Vector3(1, 1, 0);
        }
    }

    private void ValidateChoice() {
        var buttonSelected = _buttonsList[_cursorIndex];
        buttonSelected.onClick.Invoke();
    }

    private void AddHoverOnButtons() {
        for (int i = 0; i < _buttonsCount; i++) {
            var button = _buttonsList[i];

            var eventTrigger = button.gameObject.AddComponent<EventTrigger>();

            // Pointer Enter
            EventTrigger.Entry pointerEnterTrigger = new EventTrigger.Entry {
                eventID = EventTriggerType.PointerEnter,
            };

            pointerEnterTrigger.callback.AddListener((data) => {
                var pointerEventData = (PointerEventData)data;
                var txt = pointerEventData.pointerCurrentRaycast;

                var buttonHovered = txt.gameObject.GetComponentInParent<Button>();
                var buttonIndex = _buttonsList.IndexOf(buttonHovered);

                _cursorIndex = buttonIndex;
            });

            // Pointer Exit
            EventTrigger.Entry pointerExitTrigger = new EventTrigger.Entry {
                eventID = EventTriggerType.PointerExit
            };

            pointerExitTrigger.callback.AddListener((data) => {
                _cursorIndex = -1;
            });

            eventTrigger.triggers.Add(pointerEnterTrigger);
            eventTrigger.triggers.Add(pointerExitTrigger);

            if (i == 0) {
                _initialColor = button.gameObject.GetComponentInChildren<Text>().color;
            }
        }
    }
}
