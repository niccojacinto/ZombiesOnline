using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Base Slot for items and skills
public class UISlot : MonoBehaviour {

    private Image imageComponent;
    private Sprite emptySprite;
    public bool IsEmpty {
        get { return imageComponent.sprite == emptySprite; }
    }

    void Awake() {
        imageComponent = GetComponent<Image>();
        emptySprite = imageComponent.sprite;
    }

    public void SetSlot(Item _item) {
        imageComponent.sprite = _item.itemProperties.sprite;
    }

    public void ClearSlot() {
        imageComponent.sprite = emptySprite;
    }

    public void SetColor(Color newColor) {
        imageComponent.color = newColor;
    } 
}