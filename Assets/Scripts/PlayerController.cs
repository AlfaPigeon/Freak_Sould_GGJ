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
    private RagdollController ragdollController;
    void Start()
    {
        ragdollController = GetComponent<RagdollController>();
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        battleController = GetComponent<BattleController>();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        if (battleController != null && GetLeftClick() && movement.IsGrounded()) battleController.Attack();
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

    public void EnableRagdoll(bool enableRagdoll)
    {
        ragdollController.EnableRagdoll(enableRagdoll);
        RagdollCamera.SetActive(enableRagdoll);
        PlayerCamera.SetActive(!enableRagdoll);
    }

    public void OnStandUp()
    {
        ragdollController.OnStandUp();
    }

}
