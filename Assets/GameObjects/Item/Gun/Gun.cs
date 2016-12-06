using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[System.Serializable]
public struct GunProperties {
    public GameObject bulletPrefab;
    public float fireRate; // TODO: replace with rounds per minute later on.
    public int maxAmmo;

    [Range(0.0f, 90.0f)]
    public float accuracy; // accuracy of 0 is perfect, 90 is 180 degree coverage.. i think?
}

public abstract class Gun : Item, IGun {

    public Transform barrelExit;
    public GunProperties gunProperties;
    public int ammo;
    protected float fireDelay;

    public abstract void Shoot();

}
