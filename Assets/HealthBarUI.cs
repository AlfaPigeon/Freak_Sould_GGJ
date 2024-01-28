using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarUI : MonoBehaviour
{

    public GameObject BarObject;
    public Slider slider;
    private Stats stats;
    private PlayerController player;
    private void Start(){
        player = FindObjectOfType<PlayerController>();
        stats = player.GetComponent<Stats>();
    }
    public void ToggleBar(bool _value)
    {
        BarObject.SetActive(_value);
    }

    public void UpdateValue()
    {
        slider.maxValue = stats.statsSO.MaxHealth;
        slider.value = stats.CurrentHealth;
    }

    private void Update()
    {
        if(BarObject.activeSelf)UpdateValue();
    }
}
