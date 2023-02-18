using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Text NameText;
    [SerializeField] Text LevelText;
    [SerializeField] Text CurrentHealthText;
    [SerializeField] Text MaxHealthText;
    [SerializeField] Slider HealthSlider;
    [SerializeField] Image profileImage;

    private void Awake()
    {
        ArenaCombatManager.Instance.onBattleStart += Init;
        ArenaCombatManager.Instance.onBattleEnd += Dispose; // not sure if i even need this...
    }

    public void Init()
    {
        UpdateStats();
        ArenaCombatManager.Instance.onPlayerHealthChanged += UpdateStats;
    }

    public void Dispose()
    {
        ArenaCombatManager.Instance.onPlayerHealthChanged -= UpdateStats;
    }

    public void UpdateStats()
    {
        profileImage.sprite = ArenaCombatManager.Instance.PlayerSprite;
        NameText.text = ArenaCombatManager.Instance.PlayerName;
        LevelText.text = $"Lvl: {ArenaCombatManager.Instance.PlayerLevel.ToString()}";
        CurrentHealthText.text = ArenaCombatManager.Instance.PlayerCurrentHealth < 0 ? "0" : ArenaCombatManager.Instance.PlayerCurrentHealth.ToString();
        MaxHealthText.text = ArenaCombatManager.Instance.PlayerMaxHealth.ToString();
        HealthSlider.value = ArenaCombatManager.Instance.PlayerCurrentHealth < 0 ? 0 : ArenaCombatManager.Instance.PlayerCurrentHealth / 10f;
    }
}
