using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerMovement movement;
    
    private Vector2 MovementInput;
    private Vector2 MouseAxisInput;
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }else if (context.performed)
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

        }else if (context.performed)
        {
            
            MouseAxisInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            MouseAxisInput = Vector2.zero;
        }
    }

    public Vector2 GetMouseAxisInput()
    {
        return MouseAxisInput;
    }
    void Update()
    {
        
        #region Movement
        if(movement!= null)movement.Move(MovementInput);
        #endregion


        #region RotatePlayer
        

        #endregion

    }
}
