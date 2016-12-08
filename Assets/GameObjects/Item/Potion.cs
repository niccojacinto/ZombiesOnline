using UnityEngine;
using System.Collections;

public class Potion : Item {

    public override bool Use(Character c)
    {
        c.maxHealth = 200;
        c.currentHealth = 200;
        return true;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, 0.1f);
    }
}
