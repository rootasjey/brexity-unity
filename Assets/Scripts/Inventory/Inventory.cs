using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    GameObject inventoryPanel;
    GameObject slotPanel;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    ItemDatabase database;

    int slotAmount = 4;

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    private void Start() {
        database = GetComponent<ItemDatabase>();
        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = inventoryPanel.transform.Find("Slot Panel").gameObject;

        for (int i = 0; i < slotAmount; i++) {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<Slot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform);
        }

        AddItem(0);
        AddItem(1);
        //AddItem(0);
        AddItem(1);
        //AddItem(0);
        AddItem(1);
    }

    public void AddItem(int id) {
        var itemToAdd = database.FetchItemById(id);

        if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd)) {
            for (int i = 0; i < items.Count; i++) {
                if (items[i].Id == id) {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    return;
                }
            }
        }

        for (int i = 0; i < items.Count; i++) {
            if (items[i].Id == -1) {
                items[i] = itemToAdd;
                GameObject itemObject = Instantiate(inventoryItem);

                var data = itemObject.GetComponent<ItemData>();
                data.item = itemToAdd;
                data.slot = i;
                data.amount = 1;

                itemObject.transform.SetParent(slots[i].transform);
                itemObject.GetComponent<Image>().sprite = itemToAdd.Sprite;
                itemObject.transform.position = Vector2.zero;
                itemObject.name = itemToAdd.Title;
                break;
            }
        }
    }

    private bool CheckIfItemIsInInventory(Item item) {
        for (int i = 0; i < items.Count; i++) {
            if (items[i].Id == item.Id) {
                return true;
            }
        }

        return false;
    }
}
