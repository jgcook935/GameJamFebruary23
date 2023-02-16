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
        NameText.text = CombatManager.EnemyName;
        LevelText.text = $"Lvl: {CombatManager.EnemyLevel.ToString()}";
        CurrentHealthText.text = CombatManager.EnemyCurrentHealth.ToString();
        MaxHealthText.text = CombatManager.EnemyMaxHealth.ToString();
        HealthSlider.value = CombatManager.EnemyCurrentHealth;
    }
}
