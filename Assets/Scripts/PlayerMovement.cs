using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovementBase
{



    [Header("Movement Params")]
    [SerializeField] private float WalkSpeed;
    [SerializeField] private float SprintSpeed;
    [SerializeField] private Transform CameraTransform;
    public float rotationSpeed;
    private float Speed;
    private CharacterController characterController;
    private Animator animator;
    private Vector2 MovementVector;
    private Stats stats;
    private RagdollController ragdoll;
    private Rigidbody rb;
    public PlayerController player;
    private void Start()
    {
        stats = GetComponent<Stats>();
        ragdoll = GetComponent<RagdollController>();
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
            Speed = player.GetSprint()?SprintSpeed:WalkSpeed;
            Vector3 flat_cam_dir= (transform.position - new Vector3(CameraTransform.position.x,transform.position.y,CameraTransform.position.z)).normalized;
            Vector3 mov_dir = flat_cam_dir * MovementVector.y;
            Vector3 rightVector = Vector3.Cross(flat_cam_dir, Vector3.up).normalized;

            mov_dir = mov_dir + (rightVector* -MovementVector.x);

            rb.velocity = new Vector3(mov_dir.x*Speed,rb.velocity.y,mov_dir.z*Speed);
        }
     


        #endregion

        #region Orientation

        if (CanMove && rb.velocity != Vector3.zero)
        {
            Vector3 targ_vec = rb.velocity;
            targ_vec.y = 0;
            transform.forward = targ_vec;
        }
        #endregion



        #region animations
        if(MovementVector != Vector2.zero){
            
            animator.SetBool("Walk",true);
            animator.SetBool("Run",player.GetSprint());

        }else{
            animator.SetBool("Walk",false);
            animator.SetBool("Run",false);
        }

        animator.SetBool("Fall",!IsGrounded());
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
    public float JumpPower;


   public void Roll(){
        if(!IsGrounded() || animator.GetBool("Roll") || stats.stat_state == Stats.StatState.Fainted)return;
        animator.SetBool("Roll",true);
        animator.applyRootMotion = true;
        CanMove = false;
    }
   public void OnRolling(){
      
        animator.SetBool("Roll",false);
        animator.applyRootMotion = false;
        CanMove = true;
        stats.SetStamina(stats.CurrentStamina -30f);
    }
    public void Jump(){
        if(!IsGrounded())return;
        animator.SetBool("Jump",true);
    }

    public void Jumping(){
        rb.AddForce(transform.up*JumpPower);
        animator.SetBool("Jump",false);
    }
    public bool IsGrounded()
    {
        return Mathf.Abs(rb.velocity.y) < 0.05f;
    }
}
