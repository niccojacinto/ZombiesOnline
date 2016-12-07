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

	Camera selfCamera;

    void Start() {
		selfCamera = GetComponentInChildren<Camera>();
        inventory = GetComponent<Inventory>();
        uiQuickslot = FindObjectOfType<UIQuickslot>();
    }

	[Client]
	public void Equip(Gun _gun) {
		if (currentGun == _gun) return;
		Unequip();
		currentGun = _gun;
		currentGun.GetComponent<Collider>().enabled = false;
		currentGun.gameObject.SetActive(true);
		currentGun.transform.parent = selfCamera.transform;
		currentGun.transform.position = gunMountTransform.position;
		currentGun.transform.rotation = gunMountTransform.rotation;
		currentGun.GetComponent<Rigidbody>().isKinematic = true;
		CmdEquip (_gun.netId);
	}

	[Client]
	private void Unequip() {
		if (currentGun == null) return;
		currentGun.transform.parent = null;
		currentGun.gameObject.SetActive(false);
		currentGun.GetComponent<Rigidbody>().isKinematic = false;
		currentGun.GetComponent<Collider>().enabled = true;
	}

	[Command]
	public void CmdEquip(NetworkInstanceId _id){
		// Debug.Log ("CmdEquip(" + netId + ")");
		Unequip();
		currentGun = ClientScene.FindLocalObject (_id).GetComponent<Gun> ();
		currentGun.GetComponent<Collider>().enabled = false;
		currentGun.gameObject.SetActive(true);
		currentGun.transform.parent = selfCamera.transform;
		currentGun.transform.position = gunMountTransform.position;
		currentGun.transform.rotation = gunMountTransform.rotation;
		currentGun.GetComponent<Rigidbody>().isKinematic = true;
	}

	[Client]
    public void Shoot() {
		CmdShoot ();
    }

	[Command]
	public void CmdShoot() {
		if (currentGun == null) {
			Debug.Log ("PlayerID: " + netId + " Unable to Shoot because currentGun is null");
			return;
		}

		if (currentGun.Ready ()) {
			currentGun.Shoot ();
			GameObject bullet = (GameObject)Instantiate(currentGun.gunProperties.bulletPrefab, currentGun.barrelExit.position, currentGun.GetOffset());
			NetworkServer.Spawn(bullet);
		}
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
