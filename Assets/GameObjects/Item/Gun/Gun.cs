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
	
public class Gun : Item, IGun {

    public Transform barrelExit;
    public GunProperties gunProperties;
    public int ammo;
    protected float fireDelay;

	public override bool Use(Character c) { return false; }
	public virtual void Shoot(){}
	public virtual bool Ready (){ return false;}
	public virtual Quaternion GetOffset (){ return Quaternion.identity; }

}
