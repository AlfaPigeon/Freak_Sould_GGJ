using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(Animator))]
public class EnemyBattleController : MonoBehaviour
{
    private Stats stats;
    public float AttackPower;
    public bool Invulnarable = false;
    private Animator animator;
    private RagdollController ragdoll;
    private PlayerController player;

    void Start()
    {
        ragdoll = GetComponent<RagdollController>();
        stats = GetComponent<Stats>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
    }

    public void GetHit(BattleController _enemy)
    {
        if (_enemy == null) return;
        if (Invulnarable) return;
        if (stats.CurrentKnockShield <= 0)
        {
            stats.CurrentHealth = Mathf.Max(0f, stats.CurrentHealth - 2 * _enemy.AttackPower);
        }
        else
        {
            stats.CurrentHealth = Mathf.Max(0f, stats.CurrentHealth - _enemy.AttackPower);

            if (!ragdoll.GetRagdollState())
            {
                stats.CurrentKnockShield = Mathf.Max(0f, stats.CurrentKnockShield - _enemy.AttackPower);
                if (stats.CurrentKnockShield <= 0)
                {
                    ragdoll.EnableRagdoll(true);
                }
            }

        }
    }



    public void OnAttacking()
    {
        Vector3 directionToEnemy = player.transform.position - transform.position;
        float angle = Vector3.Angle(directionToEnemy, transform.forward);

        if (angle < 45 && directionToEnemy.magnitude<3f)
        {
            player.GetComponent<BattleController>().GetHit(this);
        }

        stats.SetStamina(stats.CurrentStamina -15);
    }


}
