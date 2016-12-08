using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

[RequireComponent(typeof(SoldierCharacter))]
[RequireComponent(typeof(SetupLocalPlayer))]
public class PlayerController : NetworkBehaviour, ISoldier {

    ISoldier pawn;

    Camera cam;
    float camFOV = 60.0f;
    float dampVelocity = 0.4f;
    public Transform GunPos;
    public Transform GunAimPos;

    bool lockCursor = true;

    Animator animator;
    //bool anim_isJumping = false;
    //bool anim_isGrounded = false;
    bool anim_isAiming = false;
    bool anim_isSprinting = false;
    float anim_moveSpeedX = 0.0f;
    float anim_moveSpeedZ = 0.0f;

    float walkSpeed = 0.5f;
    float sprintSpeed = 2.0f;

    void Start() {
        pawn = GetComponent<SoldierCharacter>();
		cam = GetComponentInChildren<Camera>();
        animator = GetComponent<Animator>();
        pawn.SetGunPosition(GunPos);
    }

    void Update() {
        cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, camFOV, ref dampVelocity, 0.1f);
        HandleInput();
    }

    void HandleInput() {
        HandleMovement();
        HandleMouse();
        HandleAnimations();
        HandleQuickslot();
    }

    void HandleMovement() {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            anim_isSprinting = true;
        }
        else
        {
            anim_isSprinting = false;
        }

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        anim_moveSpeedX = vertical;
        anim_moveSpeedZ = horizontal;

        if(anim_isAiming)
        {
            MoveForward(vertical * walkSpeed);
            MoveRight(horizontal * walkSpeed);
        }
        else if(anim_isSprinting)
        {
            MoveForward(vertical * sprintSpeed);
            MoveRight(horizontal * sprintSpeed);
        }
        else
        {
            MoveForward(vertical);
            MoveRight(horizontal);
        }


        float xRot = Input.GetAxis("Mouse X"); // * XSensitivity;
        float yRot = Input.GetAxis("Mouse Y"); // * YSensitivity;

        // Camera Up and Down
		UpdateCameraY(yRot);
        // Camera Rotation and Player Rotation -- Rotation speed is limited by the Character rotation speed
        RotateRight(xRot);

        UpdateCursorLock();
    }

    void HandleAnimations()
    {
        animator.SetFloat("moveSpeedx", anim_moveSpeedX);
        animator.SetFloat("moveSpeedz", anim_moveSpeedZ);

        animator.SetBool("isAiming", anim_isAiming);
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
            pawn.UseItemAtIndex(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            pawn.UseItemAtIndex(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            pawn.UseItemAtIndex(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            pawn.UseItemAtIndex(4);
        }
    }

    void HandleMouse() {
        if (Input.GetMouseButton(0)) {
            Shoot();
        }
        if(Input.GetMouseButtonDown(1))
        {
            anim_isAiming = true;
            camFOV = 30;
            pawn.SetGunPosition(GunAimPos);
        }
        if(Input.GetMouseButtonUp(1))
        {
            anim_isAiming = false;
            camFOV = 60;
            pawn.SetGunPosition(GunPos);
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

	public void UpdateCameraY(float _yRot) {
		pawn.UpdateCameraY (_yRot);
	}

    public bool UseItemAtIndex(int _index) {
        return pawn.UseItemAtIndex(_index);
    }

    public bool DropItemAtIndex(int _index) {
        return pawn.DropItemAtIndex(_index);
    }

    public void SetGunPosition(Transform _position)
    {
        pawn.SetGunPosition(_position);
    }
}
