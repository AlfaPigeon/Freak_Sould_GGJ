using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    private bool Switcher;
    private Vector3 Target;

    public override void Enter()
    {
        _stateMachine.Animator.SetFloat("ChickenWalk", 0.5f);
        _stateMachine.Animator.Play(StandardHash);
        Switcher = true;
    }

    public override void Tick(float deltaTime)
    {
        if (IsInRange(_stateMachine.Player.transform.position, _stateMachine.FocusRangeDistance))
            _stateMachine.SwitchState(new EnemyFocusState(_stateMachine));

        if (Switcher)
            Target = _stateMachine.EndPoint.position;
        else
            Target = _stateMachine.StartPoint.position;
        
        CalculateMovement(deltaTime, _stateMachine.MovementSpeed, Target);
        if (IsInRange(Target, 0.5f))
            Switcher = !Switcher;
    }

    public override void Exit()
    {
        // _stateMachine.StandardHash = Random.Range(_stateMachine.StandardHash, _stateMachine.StandardHash + 4);
        _stateMachine.OldState = States.IdleState;
    }
}