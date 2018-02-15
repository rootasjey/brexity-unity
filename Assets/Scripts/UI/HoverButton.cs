using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private Text _text;

    public void OnPointerEnter(PointerEventData eventData) {
        gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 0);

        _text.text = ">" + _text.text;
        _text.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData) {
        gameObject.transform.localScale = new Vector3(1f, 1f, 0);
        _text.text = _text.text.Substring(1);
        _text.color = Color.white;
    }

    // Use this for initialization
    void Start () {
        _text = gameObject.GetComponentInChildren<Text>();
    }
}
