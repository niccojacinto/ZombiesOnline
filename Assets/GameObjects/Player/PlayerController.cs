using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SoldierCharacter))]
[RequireComponent(typeof(SetupLocalPlayer))]
public class PlayerController : MonoBehaviour, ISoldier {

    ISoldier pawn;
    Camera selfCamera;
    bool lockCursor = true;

    void Start() {
        pawn = GetComponent<SoldierCharacter>();
        selfCamera = GetComponentInChildren<Camera>();
    }

    void Update() {
        HandleInput();
    }

    void HandleInput() {
        HandleMovement();
        HandleMouse();
        HandleQuickslot();
    }

    void HandleMovement() {
        float vertical = Input.GetAxis("Vertical");
        MoveForward(vertical);

        float horizontal = Input.GetAxis("Horizontal");
        MoveRight(horizontal);

        float xRot = Input.GetAxis("Mouse X"); // * XSensitivity;
        float yRot = Input.GetAxis("Mouse Y"); // * YSensitivity;

        // Camera Up and Down
        selfCamera.transform.localRotation *= Quaternion.Euler(-yRot, 0, 0f);
        // Camera Rotation and Player Rotation -- Rotation speed is limited by the Character rotation speed
        RotateRight(xRot);

        UpdateCursorLock();
    }

    void SetCursorLock(bool value)
    {
        lockCursor = value;
        if(!lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void UpdateCursorLock()
    {
        if(lockCursor)
        {
            InternalLockUpdate();
        }
    }

    void InternalLockUpdate()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            lockCursor = false;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            lockCursor = true;
        }

        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if(!lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void HandleQuickslot() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            pawn.UseItemAtIndex(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {

        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {

        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {

        }

        if (Input.GetKeyDown(KeyCode.Alpha5)) {

        }
    }

    void HandleMouse() {
        if (Input.GetMouseButton(0)) {
            Shoot();
        }
    }

    public void MoveForward(float _scale) {
        pawn.MoveForward(_scale);
    }

    public void MoveRight(float _scale) {
        pawn.MoveRight(_scale);
    }

    public void RotateRight(float _scale) {
        pawn.RotateRight(_scale);
    }

    public void Equip(Gun _gun) {
        pawn.Equip(_gun);
    }

    public void Shoot() {
        pawn.Shoot();
    }

    public bool UseItemAtIndex(int _index) {
        return pawn.UseItemAtIndex(_index);
    }

    public bool DropItemAtIndex(int _index) {
        return pawn.DropItemAtIndex(_index);
    }
}
