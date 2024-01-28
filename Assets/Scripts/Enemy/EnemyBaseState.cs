using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EnemyBaseState: State
{
    protected EnemyStateMachine _stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    protected readonly int StandardHash = Animator.StringToHash("StandardStateStart");
    protected readonly int FocusHash = Animator.StringToHash("FocusStateStart");
    protected readonly int AttackHash = Animator.StringToHash("AttackStateStart");
    protected readonly int BeingHitHash = Animator.StringToHash("BeingHit");

    protected readonly int WalkPercentHash = Animator.StringToHash("WalkPercent");
    protected readonly int AttackStateHash = Animator.StringToHash("AttackState");
    protected readonly int FocusStateHash = Animator.StringToHash("FocusState");
    protected readonly int RunningHash = Animator.StringToHash("Running");
    protected readonly int HitVersionHash = Animator.StringToHash("HitVersion");
    
    protected bool IsInRange(Vector3 target, float distance)
    {
        float rangeDistance = Vector3.Distance(target, _stateMachine.transform.position);
        if (rangeDistance <= distance)
            return true;
        return false;
    }

    protected void CalculateMovement(float deltaTime, float speed, Vector3 target = default)
    {
        _stateMachine.NaveMeshAgent.speed = speed;

        if (target == Vector3.zero)
        {
            _stateMachine.NaveMeshAgent.SetDestination(_stateMachine.EndPoint.position);
            return;
        }

        _stateMachine.NaveMeshAgent.SetDestination(target);
    }

    protected void CalculateRotation(float deltaTime, float speed, Vector3 target = default)
    {
        Quaternion desiredRotation =
            Quaternion.LookRotation(target == Vector3.zero ? _stateMachine.NaveMeshAgent.velocity.normalized : target - _stateMachine.transform.position);
        _stateMachine.transform.rotation =
            Quaternion.Slerp(_stateMachine.transform.rotation, desiredRotation, deltaTime * speed);
    }
    protected void CalculateRotation(float deltaTime, float speed, out float angleDiffer, out bool isNegative, Vector3 target = default)
    {
        Quaternion desiredRotation =
            Quaternion.LookRotation(target == Vector3.zero ? _stateMachine.NaveMeshAgent.velocity.normalized : target - _stateMachine.transform.position);
        _stateMachine.transform.rotation =
            Quaternion.Slerp(_stateMachine.transform.rotation, desiredRotation, deltaTime * speed);

        angleDiffer = Quaternion.Angle(_stateMachine.transform.rotation, desiredRotation);
        isNegative = Vector3.Cross(_stateMachine.transform.rotation * Vector3.forward, desiredRotation * Vector3.forward).y < 0;
    }
}
