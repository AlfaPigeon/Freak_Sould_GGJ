using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class EnemyFocusState : EnemyBaseState
{
    public EnemyFocusState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    private bool trigger = false;
    
    public override void Enter()
    {
        _stateMachine.Animator.CrossFadeInFixedTime(FocusHash, _stateMachine.CrossFadeDuration);
        // StartCoroutine(TimeCounter());
    }

    public override void Tick(float deltaTime)
    {
        if (IsInRange(_stateMachine.Player.transform.position, _stateMachine.AttackRangeDistance))
            _stateMachine.SwitchState(new EnemyAttackState(_stateMachine));
        
        if (!IsInRange(_stateMachine.Player.transform.position, _stateMachine.FocusRangeDistance))
            _stateMachine.SwitchState(new EnemyIdleState(_stateMachine));
        
        CalculateRotation(deltaTime, _stateMachine.RotationSpeed, _stateMachine.Player.transform.position);
        
        if (trigger)
            _stateMachine.SwitchState(new EnemyAttackState(_stateMachine));
    }

    public override void Exit()
    {
        _stateMachine.OldState = States.FocusState;
    }

    // private IEnumerator TimeCounter()
    // {
    //     yield return new WaitForSeconds(5);
    //     trigger = true;
    // }

    public async Task TimeCounterAsync()
    {
        await Task.Delay(200);
        trigger = true;
    }
}
