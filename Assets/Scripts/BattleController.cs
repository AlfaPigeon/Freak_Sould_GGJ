using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(Animator))]
public class BattleController : MonoBehaviour
{
    private Stats stats;
    public float AttackPower;
    public bool Invulnarable = false;

    private Animator animator;
    private PlayerController player;

    public float combo_timer;
    private float current_combo_timer;
    private PlayerMovement playerMovement;
    private RagdollController ragdoll;

    public GameObject AttackEffect;


    void Start()
    {
        ragdoll = GetComponent<RagdollController>();
        stats = GetComponent<Stats>();
        player = GetComponent<PlayerController>();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
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

    private bool Attacking = false;

    public void Attack()
    {
        if (Attacking || stats.stat_state == Stats.StatState.Fainted) return;
        Attacking = true;
        animator.SetBool("Attack", true);
        
        current_combo_timer = 0f;
        animator.applyRootMotion = true;
        if (playerMovement != null) playerMovement.CanMove = false;
    }


    public void OnAttacking()
    {
        //Hit every enemy in vicinity, if single enemy exist and yo uare behind do a back stab
        animator.SetInteger("AttackCounter", (animator.GetInteger("AttackCounter") + 1) % 3);

        Attacking = false;
        
        if (playerMovement != null) playerMovement.CanMove = true;

        stats.SetStamina(stats.CurrentStamina -15);
    }

    private void Update()
    {


        #region  Attack
        if (Attacking || (player != null && player.GetLeftClick()) || animator.GetInteger("AttackCounter") == 0) return;
        current_combo_timer += Time.deltaTime;
        if (current_combo_timer >= combo_timer)
        {
            current_combo_timer = 0f;
            animator.SetBool("Attack", false);
            animator.SetInteger("AttackCounter", 0);
        }
        #endregion
    }

}
