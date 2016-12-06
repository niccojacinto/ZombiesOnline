using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour, IInventory {
    [SerializeField]
    Item[] items;
    public int itemCapacity;

    void Start() {
        items = new Item[itemCapacity];
    }

    public bool AddItem(Item _item) {
        // Add the item in an empty slot
        // Debug.Log("Adding Item: " + _item.itemProperties.name);
        for (int i = 0; i < items.Length; ++i) {
            if (items[i] == null) {
                items[i] = _item;
                _item.gameObject.SetActive(false);
                return true;
            }
        }
        return false;
    }

    public bool DropItemAtIndex(int _index) {
        items[_index] = null;
        return true;
    }

    public bool UseItemAtIndex(int _index) {
        if (items[_index] == null) return false;
        // If the item is used up
        if (items[_index].Use(GetComponent<Character>())) {
            Destroy(items[_index].gameObject);
            items[_index] = null;
            return true;
        }
        return false;
    }

    public bool IsFull() {
        for (int i = 0; i < items.Length; ++i) {
            if (items[i] == null) {
                return false;
            }
        }
        return true;
    }

    public Item ItemAtIndex(int _index) {
        return items[_index];
    }
}
