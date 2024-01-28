
using UnityEngine;
using UnityEngine.UI;
public class StaminaBarUI : MonoBehaviour
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
        slider.maxValue = stats.statsSO.MaxStamina;
        slider.value = stats.CurrentStamina;
    }

    private void Update()
    {
        if(BarObject.activeSelf)UpdateValue();
    }
}
