using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HealthPack : Item {

	public override bool Use(Character c) {
        c.currentHealth += 20.0f;
        return true;
    }
}
