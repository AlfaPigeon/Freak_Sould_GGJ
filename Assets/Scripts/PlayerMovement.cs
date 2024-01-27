using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovementBase
{


    [Header("Options")]
    public bool CanMove = true;
    [Header("Movement Params")]
    [SerializeField] private float WalkSpeed;
    [SerializeField] private float SprintSpeed;
    [SerializeField] private Transform CameraTransform;
    public float rotationSpeed;
    private float Speed;
    private CharacterController characterController;
    private Animator animator;
    private Vector2 MovementVector;

    private Rigidbody rb;
    public PlayerController player;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        MovementVector = Vector2.zero;
    }

    private Vector3 _direction;

    public float _turnSmoothTime = 0.1f;
    public float _turnSmoothVelocity;
    void Update()
    {
        Vector3 velocity = rb.velocity;
       
        #region movement
        if (CanMove)
        {
            
            Vector3 flat_cam_dir= (transform.position - new Vector3(CameraTransform.position.x,transform.position.y,CameraTransform.position.z)).normalized;
            Vector3 mov_dir = flat_cam_dir * MovementVector.y;
            Vector3 rightVector = Vector3.Cross(flat_cam_dir, Vector3.up).normalized;

            mov_dir = mov_dir + (rightVector* -MovementVector.x);

            rb.velocity = new Vector3(mov_dir.x*WalkSpeed,rb.velocity.y,mov_dir.z*WalkSpeed);
        }
        else
        {
            velocity.x = 0;
            velocity.y = 0;
        }
        #endregion

        #region Orientation
        if (rb.velocity != Vector3.zero)
        {
            Vector3 targ_vec = rb.velocity;
            targ_vec.y = 0;
            transform.forward = targ_vec;
        }
        #endregion



        #region animations


        #endregion
  
    }

    public void Move(Vector2 moveVector)
    {
        MovementVector = moveVector;
    }

    public void ChangeSpeed(int newValue)
    {
        Speed = newValue;
    }
    public Vector2 GetMovementVector()
    {
        return MovementVector;
    }

}
