using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public enum StatState
    {
        Normal,
        Fainted,
        Death
    }
    public StatState stat_state = StatState.Normal;
    [Header("Stats Object")]
    public StatsSO statsSO;
    [Header("Current Stats")]
    public float CurrentHealth;
    public float CurrentStamina;
    public float CurrentKnockShield;
    private RagdollController ragdoll;
    void Start()
    {
        ragdoll = GetComponent<RagdollController>();
        CurrentHealth = statsSO.MaxHealth;
        CurrentStamina = statsSO.MaxHealth;
        CurrentKnockShield = statsSO.MaxKnockShield;
    }

    public void SetHealth(float _value)
    {

        if (_value <= 0)
        {
            CurrentHealth = 0f;
        }
        else
        {
            CurrentHealth = _value;
        }
        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }
    public void SetKnockdownShield(float _value)
    {
        KnockShieldCounter = 0f;
        if (_value <= 0)
        {
            CurrentKnockShield = 0f;
            stat_state = StatState.Fainted;
            ragdoll.EnableRagdoll(true);
        }
        else
        {
            CurrentKnockShield = _value;
        }
    }

    public void SetStamina(float _value)
    {
        StaminaCounter = 0f;
        if (_value <= 0)
        {
            CurrentStamina = 0f;
            stat_state = StatState.Fainted;
            ragdoll.EnableRagdoll(true);
        }
        else
        {
            CurrentStamina = _value;
        }
    }
    private float KnockShieldCounter;
    private float StaminaCounter;
    void Update()
    {
        if (stat_state == StatState.Death) return;
        if (CurrentKnockShield < statsSO.MaxKnockShield)
        {
            KnockShieldCounter += Time.deltaTime;
            if (KnockShieldCounter > 3f)
            {
                CurrentKnockShield = Mathf.Min(CurrentKnockShield + 30 * Time.deltaTime, statsSO.MaxKnockShield);
            }
        }
        else
        {
            KnockShieldCounter = 0f;
        }
        if (CurrentStamina < statsSO.MaxStamina)
        {
            StaminaCounter += Time.deltaTime;
            if (StaminaCounter > 3f)
            {
                CurrentStamina = Mathf.Min(CurrentStamina + 30 * Time.deltaTime, statsSO.MaxStamina);
            }
        }
        else
        {
            StaminaCounter = 0f;
        }

        if (stat_state == StatState.Fainted)
        {
            if (ragdoll.GetRagdollState() && CurrentStamina >= statsSO.MaxStamina && CurrentKnockShield >= statsSO.MaxKnockShield)
            {
                ragdoll.EnableRagdoll(false);
                stat_state = StatState.Normal;
            }
        }

    }


    public void Die()
    {
        stat_state = StatState.Death;
        ragdoll.EnableRagdoll(true);
        EnemyEventHandler enemyEventHandler = GetComponent<EnemyEventHandler>();
        if (enemyEventHandler != null)
        {
            Destroy(enemyEventHandler);
        }

        EnemyStateMachine state_machine = GetComponent<EnemyStateMachine>();
        if (state_machine != null)
        {
            Destroy(state_machine);
        }
        Destroy(gameObject, 3f);

    }
}
