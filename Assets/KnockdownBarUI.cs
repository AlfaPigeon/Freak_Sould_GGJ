using UnityEngine;
using UnityEngine.UI;

public class KnockdownBarUI : MonoBehaviour
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
        slider.maxValue = stats.statsSO.MaxKnockShield;
        slider.value = Mathf.Lerp(slider.value ,stats.CurrentKnockShield,Time.deltaTime);
    }

    private void Update()
    {
        if(BarObject.activeSelf)UpdateValue();
        if(stats.statsSO.MaxKnockShield > stats.CurrentKnockShield){
            ToggleBar(true);
        }else if(Mathf.Abs(slider.value - slider.maxValue) < 0.1f){
            ToggleBar(false);
        }
    }
}
