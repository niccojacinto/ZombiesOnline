using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Character : NetworkBehaviour, ICharacter {

    protected Transform selfTransform;
    protected Rigidbody selfRigidbody;

    public const float linearSpeed = 5.0f; // Units per second
    public const float angularSpeed = 180.0f; // Degrees per second

    public const float maxHealth = 100.0f;
    [SyncVar]
    public float currentHealth = 100.0f;
    public bool destroyOnDeath = true;
    private NetworkStartPosition[] spawnPoints;
    public GameObject UIHealthBarPrefab;
    Slider UIHealthBar;

    public virtual void Awake() {
        // Cache components for speed and efficiency
        selfTransform = transform;
        selfRigidbody = GetComponent<Rigidbody>();
        
        // Prevent rigidbody from tipping over 
        selfRigidbody.freezeRotation = true;
    }

    public virtual void Start()
    {
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
            Canvas HUD = FindObjectOfType<Canvas>();
            UIHealthBar = Instantiate(UIHealthBarPrefab).GetComponent<Slider>();
            UIHealthBar.transform.SetParent(HUD.transform);
        }
    }

    public void MoveForward(float _scale) {
        selfTransform.Translate(Vector3.forward * linearSpeed * _scale * Time.deltaTime);
    }

    public void MoveRight(float _scale) {
        selfTransform.Translate(Vector3.right * linearSpeed * _scale * Time.deltaTime);
    }

    public void RotateRight(float _scale) {
        selfTransform.Rotate(Vector3.up * angularSpeed * _scale * Time.deltaTime);
    }

    public virtual void TakeDamage(float amount)
    {
        if(!isServer)
        {
            return;
        }

        currentHealth -= amount;              
        if(currentHealth <= 0.0f)
        {
            currentHealth = 0.0f;
            if(destroyOnDeath)
            {
                RpcDestroy();
            }
            else
            {                
                currentHealth = maxHealth;
                RpcRespawn();
            }
        }
        if (!destroyOnDeath)
        {
            UIHealthBar.value = currentHealth;
        }
    }

    [ClientRpc]
    public void RpcDestroy()
    {
        FindObjectOfType<EnemySpawner>().EnemyDied();
        NetworkServer.Destroy(this.gameObject);
    }

    [ClientRpc]
    public void RpcRespawn()
    {
        if(isLocalPlayer)
        {
            Vector3 spawnPoint = Vector3.zero;

            if(spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].transform.position;
            }

            transform.position = spawnPoint;
        } 
    }
}
