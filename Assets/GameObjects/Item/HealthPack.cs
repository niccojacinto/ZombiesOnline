using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HealthPack : Item {

	public override bool Use(Character c) {
        c.health += 20.0f;
        return true;
    }
}
