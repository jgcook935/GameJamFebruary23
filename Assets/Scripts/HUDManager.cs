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
        NameText.text = CombatManager.PlayerName;
        LevelText.text = $"Lvl: {CombatManager.PlayerLevel.ToString()}";
        CurrentHealthText.text = CombatManager.PlayerCurrentHealth.ToString();
        MaxHealthText.text = CombatManager.PlayerMaxHealth.ToString();
        HealthSlider.value = CombatManager.PlayerCurrentHealth;
    }
}
