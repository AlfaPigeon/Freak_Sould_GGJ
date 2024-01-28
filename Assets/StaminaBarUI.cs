using UnityEngine;
using UnityEngine.UI;
public class StaminaBarUI : MonoBehaviour
{
    public GameObject BarObject;
    public Slider slider;
    public Stats stats;
    public void ToggleBar(bool _value)
    {
        BarObject.SetActive(_value);
    }

    public void UpdateValue()
    {
        slider.maxValue = stats.statsSO.MaxStamina;
        slider.value = Mathf.Lerp(slider.value ,stats.CurrentStamina,Time.deltaTime);
    }

    private void Update()
    {
        if(BarObject.activeSelf)UpdateValue();
    }
}
