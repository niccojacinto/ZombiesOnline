using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class M16 : Gun {


    void Start() {
        ammo = gunProperties.maxAmmo;
        fireDelay = 0.0f;
    }

    public override bool Use(Character c) {
        SoldierCharacter sc = c as SoldierCharacter;
        sc.Equip(this);
        return false;
    }
		
	public override void Shoot() {
		fireDelay = gunProperties.fireRate;
	}

	public override bool Ready() {
		return fireDelay <= 0;
	}

	public override Quaternion GetOffset() {
        Vector3 v = transform.rotation.eulerAngles;
        v = new Vector3(v.x + Random.Range(-gunProperties.accuracy, gunProperties.accuracy),
                        v.y + Random.Range(-gunProperties.accuracy, gunProperties.accuracy), 
                        + v.z);
        return Quaternion.Euler(v);
    }

    void Update () {
        fireDelay -= Time.deltaTime;
    }
}
