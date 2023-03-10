using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractSlider : MonoBehaviour
{
        public float targetHP = 0f;
    public float maxHP = 10f;
    private float sliderLerpSpeed = 200f;
    private float currentVelocity = 0f;
    private bool init = false;    

    protected void UpdateSlider(Slider slider, Image hpColorImage)
    {
        if(!init)
        {
            SetSliderValue(slider, targetHP);
            init = true;
        }
        else
        {
            SetSliderValue(slider, Mathf.SmoothDamp(slider.value, targetHP, ref currentVelocity, sliderLerpSpeed * Time.deltaTime));
        }

        if((slider.value * maxHP) < 0.5f * maxHP)
        {
            hpColorImage.color = Color.red;
        } 
        else hpColorImage.color = new Color(29f / 255f, 168f / 255f, 6f / 255f);
    }

    private void SetSliderValue(Slider slider, float value)
    {
        slider.value = Mathf.Max(value, 0f);
    }
}