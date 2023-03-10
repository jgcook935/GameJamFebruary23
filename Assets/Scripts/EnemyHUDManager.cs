using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUDManager : MonoBehaviour
{
    [SerializeField] Text NameText;
    [SerializeField] Text LevelText;
    [SerializeField] Text CurrentHealthText;
    [SerializeField] Text MaxHealthText;
    [SerializeField] Slider HealthSlider;
    [SerializeField] Image profileImage;
    [SerializeField] Image hpColorImage;

    private void Awake()
    {
        ArenaCombatManager.Instance.onBattleStart += Init;
        ArenaCombatManager.Instance.onBattleEnd += Dispose; // not sure if i even need this...
    }

    public void Init()
    {
        UpdateEnemyStats();
        ArenaCombatManager.Instance.onEnemyHealthChanged += UpdateEnemyStats;
    }

    public void Dispose()
    {
        ArenaCombatManager.Instance.onEnemyHealthChanged -= UpdateEnemyStats;
    }

    public void UpdateEnemyStats()
    {
        profileImage.sprite = ArenaCombatManager.Instance.EnemySprite;
        NameText.text = ArenaCombatManager.Instance.EnemyName;
        LevelText.text = $"Lvl: {ArenaCombatManager.Instance.EnemyLevel.ToString()}";
        CurrentHealthText.text = ArenaCombatManager.Instance.EnemyCurrentHealth < 0 ? "0" : ArenaCombatManager.Instance.EnemyCurrentHealth.ToString();
        MaxHealthText.text = ArenaCombatManager.Instance.EnemyMaxHealth.ToString();
        HealthSlider.value = ArenaCombatManager.Instance.EnemyCurrentHealth < 0 ? 0 : ArenaCombatManager.Instance.EnemyCurrentHealth / 10f;
        if(ArenaCombatManager.Instance.EnemyCurrentHealth < 0.5f * ArenaCombatManager.Instance.EnemyMaxHealth)
        {
            hpColorImage.color = Color.red;
        } 
        else hpColorImage.color = new Color(29f / 255f, 168f / 255f, 6f / 255f);
    }
}
