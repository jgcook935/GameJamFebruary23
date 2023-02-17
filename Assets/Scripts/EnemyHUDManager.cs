using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUDManager : MonoBehaviour
{
    [SerializeField] ArenaCombatManager CombatManager;

    [SerializeField] Text NameText;
    [SerializeField] Text LevelText;
    [SerializeField] Text CurrentHealthText;
    [SerializeField] Text MaxHealthText;
    [SerializeField] Slider HealthSlider;

    void Awake()
    {
        UpdateEnemyStats();
        CombatManager.onEnemyHealthChanged += UpdateEnemyStats;
    }

    void OnDestroy()
    {
        CombatManager.onEnemyHealthChanged -= UpdateEnemyStats;
    }

    public void UpdateEnemyStats()
    {
        NameText.text = CombatManager.EnemyName;
        LevelText.text = $"Lvl: {CombatManager.EnemyLevel.ToString()}";
        CurrentHealthText.text = CombatManager.EnemyCurrentHealth < 0 ? "0" : CombatManager.EnemyCurrentHealth.ToString();
        MaxHealthText.text = CombatManager.EnemyMaxHealth.ToString();
        HealthSlider.value = CombatManager.EnemyCurrentHealth < 0 ? 0 : CombatManager.EnemyCurrentHealth / 10f;
    }
}
