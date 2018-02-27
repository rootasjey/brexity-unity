using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public InvItem item;

    private float _factor = 1.1f;

    public void OnPointerEnter(PointerEventData eventData) {
        var rect = GetComponent<RectTransform>();

        rect.localScale = new Vector3(
            rect.localScale.x * _factor, 
            rect.localScale.y * _factor, 
            rect.localScale.z * _factor);
    }

    public void OnPointerExit(PointerEventData eventData) {
        var rect = GetComponent<RectTransform>();

        rect.localScale = new Vector3(
            rect.localScale.x / _factor,
            rect.localScale.y / _factor,
            rect.localScale.z / _factor);
    }
}
