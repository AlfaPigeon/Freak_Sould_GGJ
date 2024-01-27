using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    public EnemyDeathState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.Animator.CrossFadeInFixedTime(BeingHitHash, _stateMachine.CrossFadeDuration);
        _stateMachine.Animator.SetFloat(HitVersionHash, Random.Range(0, 3) / 2);
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}