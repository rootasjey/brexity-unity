using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, 
    IPointerEnterHandler, IPointerExitHandler {

    public Item item;
    public int amount;
    public int slot;

    private Vector2 offset;
    private Inventory inventory;
    private Tooltip _tooltip;

    void Start() {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        _tooltip = inventory.GetComponent<Tooltip>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (item == null) {
            return;
        }

        offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
        this.transform.SetParent(this.transform.parent.parent); // inventory
        this.transform.position = eventData.position;

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        if (item == null) {
            return;
        }

        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        this.transform.SetParent(inventory.slots[slot].transform);
        this.transform.position = inventory.slots[slot].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    //public void OnPointerDown(PointerEventData eventData) {
        
    //}

    public void OnPointerEnter(PointerEventData eventData) {
        _tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData) {
        _tooltip.Deactivate();
    }
}
