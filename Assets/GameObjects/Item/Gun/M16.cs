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
        CmdShoot();
    }


    [Command]
    public void CmdShoot() {
        if (ammo <= 0 || fireDelay > 0) return;
        fireDelay = gunProperties.fireRate;
        GameObject bullet = Instantiate(gunProperties.bulletPrefab, barrelExit.position, AccuracyOffset()) as GameObject;
        // Debug.Log(AccuracyOffset());
        NetworkServer.Spawn(bullet);
        --ammo;
    }

    private Quaternion AccuracyOffset() {
        Vector3 v = transform.rotation.eulerAngles;
        v = new Vector3(v.x + Random.Range(-gunProperties.accuracy, gunProperties.accuracy),
                        v.y + Random.Range(-gunProperties.accuracy, gunProperties.accuracy), 
                        v.z);
        return Quaternion.Euler(v);
    }

    void Update () {
        fireDelay -= Time.deltaTime;
    }
}
