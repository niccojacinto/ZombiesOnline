using UnityEngine;
using System.Collections;

public class EnergyPack : Item {

    public override bool Use(Character c)
    {
        c.speedModifier = 2.0f;
        return true;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, 0.1f);
    }
}
