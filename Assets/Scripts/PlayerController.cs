using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerMovement movement;

    private Vector2 MovementInput;
    private Vector2 MouseAxisInput;
    private Animator animator;
    private Rigidbody rb;
    private BattleController battleController;
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        battleController = GetComponent<BattleController>();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Init();
        EnableRagdoll(false);
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
        else if (context.performed)
        {
            MovementInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            MovementInput = Vector2.zero;
        }
    }
    public void OnMouse(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
        else if (context.performed)
        {

            MouseAxisInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            MouseAxisInput = Vector2.zero;
        }
    }
    private bool sprint;
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
        else if (context.performed)
        {
            sprint = context.action.IsPressed();
        }
        else if (context.canceled)
        {
            sprint = false;
        }
    }

    public bool GetSprint()
    {
        return sprint;
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
        else if (context.performed)
        {
            movement.Jump();
        }
        else if (context.canceled)
        {

        }
    }

    private bool LeftClick = false;
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
        else if (context.performed)
        {
            LeftClick = context.action.IsPressed();
        }
        else if (context.canceled)
        {
            LeftClick = false;
        }
    }
    public bool GetLeftClick()
    {
        return LeftClick;
    }

    public Vector2 GetMouseAxisInput()
    {
        return MouseAxisInput;
    }
    void Update()
    {

        #region Movement
        if (movement != null) movement.Move(MovementInput);
        #endregion


        #region Attack
        if(battleController != null && GetLeftClick())battleController.Attack();
        #endregion

    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
        else if (context.performed)
        {
            movement.Roll();
        }
        else if (context.canceled)
        {

        }
    }
    public void OnRagdoll(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
        else if (context.performed)
        {
            EnableRagdoll(context.action.IsPressed());
        }
        else if (context.canceled)
        {
            EnableRagdoll(false);
        }
    }

    [Header("Ragdoll")]

    public GameObject RagdollCamera;
    public GameObject PlayerCamera;
    public Transform Anchor;

    public RuntimeAnimatorController PlayerAnimatorController;
    public RuntimeAnimatorController RagdollAnimatorController;
    public Collider[] colliderToEnable;




    // all colliders that are activated when using ragdoll
    Collider[] allCollider;

    // all the rigidbodies used by ragdoll
    List<Rigidbody> ragdollRigidBodies;


    /// <summary>
    /// this stores reference of all the collider and attached rigidbodies used by ragdoll
    /// </summary>
    private void Init()
    {
        ragdollRigidBodies = new List<Rigidbody>();
        allCollider = GetComponentsInChildren<Collider>(true); // get all the colliders that are attached
        foreach (var collider in allCollider)
        {
            if (collider.transform != transform) // if this is not parent transform
            {
                var rag_rb = collider.GetComponent<Rigidbody>(); // get attached rigidbody
                if (rag_rb)
                {
                    ragdollRigidBodies.Add(rag_rb); // add to list
                }
            }
        }
    }
    public void EnableRagdoll(bool enableRagdoll)
    {
        Vector3 prev_velocity = rb.velocity;
        animator.enabled = !enableRagdoll;
        foreach (Collider item in allCollider)
        {
            item.enabled = enableRagdoll; // enable all colliders  if ragdoll is set to enabled
        }

        foreach (var ragdollRigidBody in ragdollRigidBodies)
        {
            ragdollRigidBody.useGravity = enableRagdoll; // make rigidbody use gravity if ragdoll is active
            ragdollRigidBody.isKinematic = !enableRagdoll; // enable or disable kinematic accordig to enableRagdoll variable
        }

        foreach (Collider item in colliderToEnable)
        {
            item.enabled = !enableRagdoll; // flip the normal colliders active state
        }
        rb.useGravity = !enableRagdoll; // normal rigidbody dont use gravity when ragdoll is active
        rb.isKinematic = enableRagdoll;

        if (enableRagdoll)
        {
            foreach (var ragdollRigidBody in ragdollRigidBodies)
            {
                ragdollRigidBody.velocity = prev_velocity*1.1f;
            }

            animator.runtimeAnimatorController = RagdollAnimatorController;
            movement.CanMove = false;
        }else{
            Vector3 _pos = Anchor.transform.position;
            _pos.y = transform.position.y;
            transform.position = _pos;
        }

        RagdollCamera.SetActive(enableRagdoll);
        PlayerCamera.SetActive(!enableRagdoll);
      
    }

    public void OnStandUp(){
        animator.runtimeAnimatorController = PlayerAnimatorController;
        movement.CanMove = true;
    }
}
