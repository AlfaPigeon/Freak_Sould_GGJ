using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Stats Object")]
    public StatsSO statsSO;
    [Header("Current Stats")]
    public float CurrentHealth;
    public float CurrentStamina;
    public float CurrentKnockShield;
    void Start()
    {
        CurrentHealth = statsSO.MaxHealth;
        CurrentStamina = statsSO.MaxHealth;
        CurrentKnockShield = statsSO.MaxKnockShield;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
