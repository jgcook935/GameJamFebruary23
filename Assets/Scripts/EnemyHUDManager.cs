using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUDManager : AbstractSlider
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

    private void Update()
    {
        UpdateSlider(HealthSlider, hpColorImage);
    }

    public void Init()
    {
        HealthSlider.value = ArenaCombatManager.Instance.EnemyCurrentHealth / ArenaCombatManager.Instance.EnemyMaxHealth;
        UpdateEnemyStats();
        ArenaCombatManager.Instance.onEnemyHealthChanged += UpdateEnemyStats;
    }

    public void Dispose()
    {
        ArenaCombatManager.Instance.onEnemyHealthChanged -= UpdateEnemyStats;
    }

    public void UpdateEnemyStats()
    {
        maxHP = ArenaCombatManager.Instance.EnemyMaxHealth;
        targetHP = ArenaCombatManager.Instance.EnemyCurrentHealth < 0 ? 0 : ArenaCombatManager.Instance.EnemyCurrentHealth / maxHP;
        profileImage.sprite = ArenaCombatManager.Instance.EnemySprite;
        NameText.text = ArenaCombatManager.Instance.EnemyName;
        LevelText.text = $"Lvl: {ArenaCombatManager.Instance.EnemyLevel.ToString()}";
        CurrentHealthText.text = ArenaCombatManager.Instance.EnemyCurrentHealth < 0 ? "0" : ArenaCombatManager.Instance.EnemyCurrentHealth.ToString();
        MaxHealthText.text = ArenaCombatManager.Instance.EnemyMaxHealth.ToString();
    }


}
