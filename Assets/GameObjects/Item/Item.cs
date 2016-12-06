using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[System.Serializable]
public struct ItemProperties {
    public GameObject prefab;
    public Sprite sprite;
    public string name;
}

[RequireComponent(typeof(Rigidbody))]
public abstract class Item : NetworkBehaviour {
    public ItemProperties itemProperties;
    public abstract bool Use(Character c);
}
