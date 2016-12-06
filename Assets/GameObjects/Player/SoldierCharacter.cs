using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

[RequireComponent(typeof(Inventory))]
public class SoldierCharacter : Character, ISoldier, IInventory {

    public Transform gunMountTransform;
    UIQuickslot uiQuickslot;
    Inventory inventory;
    Gun currentGun;

    void Start() {
        inventory = GetComponent<Inventory>();
        uiQuickslot = FindObjectOfType<UIQuickslot>();
    }

    public void Equip(Gun _gun) {
        if (currentGun == _gun) return;
        Unequip();
        currentGun = _gun;
        currentGun.GetComponent<Collider>().enabled = false;
        currentGun.gameObject.SetActive(true);
        currentGun.transform.parent = GetComponentInChildren<Camera>().transform;
        currentGun.transform.position = gunMountTransform.position;
        currentGun.transform.rotation = gunMountTransform.rotation;
        currentGun.GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void Unequip() {
        if (currentGun == null) return;
        currentGun.transform.parent = null;
        currentGun.gameObject.SetActive(false);
        currentGun.GetComponent<Rigidbody>().isKinematic = false;
        currentGun.GetComponent<Collider>().enabled = true;
    }

    public void Shoot() {
        if (currentGun == null) return;
        currentGun.Shoot();
    }

    public bool AddItem(Item _item) {
        if (inventory.AddItem(_item)) {
            uiQuickslot.RefreshUI();
            return true;
        }
        return false;
    }

    public bool DropItemAtIndex(int _index) {
        if (inventory.DropItemAtIndex(_index)) {
            uiQuickslot.RefreshUI();
            return true;
        }
        return false;
    }

    public bool UseItemAtIndex(int _index) {
        if (inventory.UseItemAtIndex(_index)) {
            uiQuickslot.RefreshUI();
            return true;
        }
        return false;
    }

    void OnCollisionEnter(Collision col) {
        Item item = col.gameObject.GetComponent<Item>();
        if (item != null) {
            AddItem(item);
        }
    }
}
