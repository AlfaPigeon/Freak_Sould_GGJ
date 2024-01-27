using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStateMachine : StateMachine
{
    //Components
    [field: SerializeField] public NavMeshAgent NaveMeshAgent { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    
    // Constants
    [field: SerializeField] public float FocusRangeDistance { get; private set; }
    [field: SerializeField] public float AttackRangeDistance { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public float CrossFadeDuration { get; private set; }
    
    //Calculated
    [field: SerializeField] public Transform EndPoint { get; set; }
    [field: SerializeField] public Transform StartPoint { get; set; }
    [field: SerializeField] public States StartState { get; set; }
    // [field: SerializeField] public int StandardHash { get; set; }
    
    //Common
    public GameObject Player { get; private set; }
    public Transform Camera { get; private set; }
    
    //Cheese
    [field: NonSerialized]public States OldState;
    
    //Permission
    [field: SerializeField] public bool IsMovable { get; set; }

    private void Awake()
    {
        transform.position = StartPoint.position;
        transform.rotation = StartPoint.rotation;
    }

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        this.Camera = UnityEngine.Camera.main.transform;
        switch(StartState)
        {
            case States.IdleState:
                SwitchState(new EnemyIdleState(this));
                OldState = States.IdleState;
                break;
            case States.FocusState:
                SwitchState(new EnemyFocusState(this));
                OldState = States.FocusState;
                break;
            case States.AttackState:
                SwitchState(new EnemyAttackState(this));
                OldState = States.IdleState;
                break;
            case States.TrapState:
                SwitchState(new EnemyTrapState(this));
                OldState = States.IdleState;
                break;
        }
    }
}

public enum States
{
    IdleState = 1,
    FocusState = 2,
    AttackState = 3,
    TrapState = 4,
}
