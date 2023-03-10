using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractSlider : MonoBehaviour
{
        public float targetHP = 0f;
    public float maxHP = 10f;
    private float sliderLerpSpeed = 200f;
    private float currentVelocity = 0f;
    private bool init = false;    

    protected void UpdateSlider(Slider HealthSlider, Image hpColorImage)
    {
        if(!init)
        {
            HealthSlider.value = targetHP;
            init = true;
        }
        else
        {
            HealthSlider.value = Mathf.SmoothDamp(HealthSlider.value, targetHP, ref currentVelocity, sliderLerpSpeed * Time.deltaTime);
        }

        if((HealthSlider.value * maxHP) < 0.5f * maxHP)
        {
            hpColorImage.color = Color.red;
        } 
        else hpColorImage.color = new Color(29f / 255f, 168f / 255f, 6f / 255f);
    }
}