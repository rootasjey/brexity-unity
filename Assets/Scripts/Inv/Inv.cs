using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inv : MonoBehaviour {
    public int slots;
    public GameObject invSlotPrefab;

    private List<GameObject> listInvSlots;

	// Use this for initialization
	void Start () {
        InstanciateNewSlots();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void InstanciateNewSlots() {
        var slotsContainer = transform.Find("Slots");

        listInvSlots = new List<GameObject>(slots);

        for (int i = 0; i < slots; i++) {
            var slot = Instantiate(invSlotPrefab, slotsContainer);
            var invSlot = slot.GetComponent<InvSlot>();

            var rect = slot.GetComponent<RectTransform>();
            rect.position = new Vector3(-5, (i * rect.sizeDelta.y) - 1f);

            listInvSlots.Add(slot);
        }
    }

    public void Add() {

    }

    public void Remove() {

    }

    private void CheckEmptySlot() {

    }

    private void Contains() {

    }
}
