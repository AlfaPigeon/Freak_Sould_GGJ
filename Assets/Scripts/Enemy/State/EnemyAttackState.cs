using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.Animator.CrossFadeInFixedTime(AttackHash, _stateMachine.CrossFadeDuration);
        _stateMachine.Target.position = _stateMachine.Player.transform.position;
    }

    public override void Tick(float deltaTime)
    {
        if (IsInRange(_stateMachine.Target.position, _stateMachine.AttackDistance))
        {
            if (_stateMachine.Animator.GetInteger(AttackStateHash) == 0)
                _stateMachine.Animator.SetInteger(AttackStateHash, Random.Range(1, 5));
            // else
            //     _stateMachine.Animator.SetInteger(AttackStateHash,
            //         Random.Range(_stateMachine.Animator.GetInteger(AttackStateHash) < 5 ? 5 : 1,
            //             _stateMachine.Animator.GetInteger(AttackStateHash) < 5 ? 9 : 5));
            CalculateMovement(deltaTime, _stateMachine.AttackSpeed, _stateMachine.Target.position);
            CalculateRotation(deltaTime, _stateMachine.AttackSpeed, _stateMachine.Target.position);
        }
        else if (IsInRange(_stateMachine.Target.position, _stateMachine.AttackRangeDistance) &&
                 !IsInRange(_stateMachine.Target.position, _stateMachine.AttackDistance))
        {
            Debug.Log(_stateMachine.NaveMeshAgent.velocity.magnitude);
            _stateMachine.Animator.SetFloat(RunningHash, _stateMachine.NaveMeshAgent.velocity.magnitude/_stateMachine.NaveMeshAgent.speed);
            _stateMachine.Animator.SetInteger(AttackStateHash, 0);
            CalculateMovement(deltaTime, _stateMachine.CaptureSpeed, _stateMachine.Target.position);
            CalculateRotation(deltaTime, _stateMachine.CaptureSpeed, _stateMachine.Target.position);
        }
        else
        {
            _stateMachine.SwitchState(new EnemyFocusState(_stateMachine));
        }
    }

    public override void Exit()
    {
        _stateMachine.Animator.SetInteger(AttackStateHash, 0);
        _stateMachine.OldState = States.AttackState;
    }
    
}