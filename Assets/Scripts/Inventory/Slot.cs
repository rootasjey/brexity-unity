using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {
    public int id;
    private Inventory inventory;

    void Start() {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData) {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();

        if (inventory.items[id].Id == -1) {
            inventory.items[droppedItem.slot] = new Item();
            inventory.items[id] = droppedItem.item;
            droppedItem.slot = id;

        } else if (droppedItem.slot != id) {
            Transform itemTransform = this.transform.GetChild(0);
            itemTransform.GetComponent<ItemData>().slot = droppedItem.slot;
            itemTransform.transform.SetParent(inventory.slots[droppedItem.slot].transform);
            itemTransform.transform.position = inventory.slots[droppedItem.slot].transform.position;

            droppedItem.slot = id;
            droppedItem.transform.SetParent(transform);
            droppedItem.transform.position = transform.position;

            inventory.items[droppedItem.slot] = droppedItem.GetComponent<ItemData>().item;
            inventory.items[id] = droppedItem.item;
        }
    }
}
