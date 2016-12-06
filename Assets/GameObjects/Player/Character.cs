using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class Character : NetworkBehaviour, ICharacter {

    protected Transform selfTransform;
    protected Rigidbody selfRigidbody;

    public const float maxHealth = 100.0f;
    public const float linearSpeed = 5.0f; // Units per second
    public const float angularSpeed = 180.0f; // Degrees per second
    public float health = 100.0f;

    void Awake() {
        // Cache components for speed and efficiency
        selfTransform = transform;
        selfRigidbody = GetComponent<Rigidbody>();
        
        // Prevent rigidbody from tipping over 
        selfRigidbody.freezeRotation = true;
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
}
