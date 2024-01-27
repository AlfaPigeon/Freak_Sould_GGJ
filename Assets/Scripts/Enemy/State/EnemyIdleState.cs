using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    private bool Switcher;
    private Vector3 Target;

    private float angleDiffer;
    private bool isNegative;
    private float walkPercent;

    public override void Enter()
    {
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
        
        CalculateRotation(deltaTime, _stateMachine.RotationSpeed, out angleDiffer, out isNegative, Target);
        
        if (isNegative & angleDiffer > 15)
            walkPercent = ((angleDiffer / 360) * 2);
        else if (!isNegative & angleDiffer > 15)
            walkPercent = ((360 - angleDiffer) / 360);
        else
            walkPercent = 1;
        
        _stateMachine.Animator.SetFloat(WalkPercentHash, walkPercent);
    }

    public override void Exit()
    {
        // _stateMachine.StandardHash = Random.Range(_stateMachine.StandardHash, _stateMachine.StandardHash + 4);
        _stateMachine.OldState = States.IdleState;
    }
}