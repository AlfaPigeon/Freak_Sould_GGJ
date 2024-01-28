using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RagdollController : MonoBehaviour
{

    private Animator animator;
    private Rigidbody rb;
    private MovementBase movement;
    private void Start()
    { 
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<MovementBase>();
        Init();
    }
    [Header("Ragdoll")]


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
        if(enableRagdoll)RagdollState = true;
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
                ragdollRigidBody.velocity = prev_velocity * 1.1f;
            }

            animator.runtimeAnimatorController = RagdollAnimatorController;
            movement.CanMove = false;
        }
        else
        {
            Vector3 _pos = Anchor.transform.position;
            _pos.y = transform.position.y;
            transform.position = _pos;
        }


    }

    public void OnStandUp()
    {
        animator.runtimeAnimatorController = PlayerAnimatorController;
        movement.CanMove = true;
        RagdollState = false;
    }

    private bool RagdollState = false;
    public bool GetRagdollState(){
        return RagdollState;
    }
}
