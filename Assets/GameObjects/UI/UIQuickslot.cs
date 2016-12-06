using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIQuickslot : UISlotGrid {

    Inventory boundInventory;

    public void BindInventory(Inventory _inventory) {
        boundInventory = _inventory;
        if (boundInventory == null) Debug.Log("Unable to bind inventory");
    }

	public void RefreshUI() {
        // Debug.Log("Refreshing UIQuickslot");
        for (int i = 0; i < boundInventory.itemCapacity; ++i) {
            if (boundInventory.ItemAtIndex(i) == null) {
                slots[i].ClearSlot();
            }
            else {
                slots[i].SetSlot(boundInventory.ItemAtIndex(i));
            }
        }
    }
}
