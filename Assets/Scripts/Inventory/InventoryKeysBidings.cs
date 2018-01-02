using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryKeysBidings : MonoBehaviour {
    GameObject _inventory;

    // Use this for initialization
    private void Start() {
        _inventory = GameObject.Find("Inventory System");
        _inventory.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Inventory")) {
            if (_inventory.activeSelf) {
                _inventory.SetActive(false);
            } else {
                _inventory.SetActive(true);
            }
        }
	}
}
