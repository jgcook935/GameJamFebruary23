using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] ArenaCombatManager CombatManager;

    [SerializeField] Text NameText;
    [SerializeField] Text LevelText;
    [SerializeField] Text CurrentHealthText;
    [SerializeField] Text MaxHealthText;
    [SerializeField] Slider HealthSlider;

    void Awake()
    {
        UpdateStats();
        CombatManager.onPlayerHealthChanged += UpdateStats;
    }

    void OnDestroy()
    {
        CombatManager.onPlayerHealthChanged -= UpdateStats;    
    }

    public void UpdateStats()
    {
        NameText.text = CombatManager.PlayerName;
        LevelText.text = $"Lvl: {CombatManager.PlayerLevel.ToString()}";
        CurrentHealthText.text = CombatManager.PlayerCurrentHealth < 0 ? "0" : CombatManager.PlayerCurrentHealth.ToString();
        MaxHealthText.text = CombatManager.PlayerMaxHealth.ToString();
        HealthSlider.value = CombatManager.PlayerCurrentHealth < 0 ? 0 : CombatManager.PlayerCurrentHealth / 10f;
    }
}
