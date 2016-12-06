using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(GridLayoutGroup))]
public class UISlotGrid : MonoBehaviour {

    public GameObject uiSlotPrefab;

    public int numColumns;
    public int numRows;
    public List<UISlot> slots;
    void Start() {
        // Create the Grid of slots
        CreateSlotGrid();
    }

    void CreateSlotGrid() {
        RectTransform rectTransform = GetComponent<RectTransform>();

        float slotSize = rectTransform.sizeDelta.x / numColumns; // figure out the size of each slot by using the panel width as base
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(slotSize, slotSize); // set the size of each slot
        // Create the actual grid
        int numSlots = numRows * numColumns;
        for (int i = 0; i < numSlots; ++i) {
            GameObject tmp = Instantiate(uiSlotPrefab, transform) as GameObject;
            UISlot newSlot = tmp.GetComponent<UISlot>();
            slots.Add(newSlot);
        }

        // Resize Height of the Grid to fit with the width;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, slotSize * numRows);

    }

    public UISlot FindEmptySlot() {
        foreach (UISlot slot in slots) {
            if (slot.IsEmpty) {
                return slot;
            }
        }
        return null;
    }
}
