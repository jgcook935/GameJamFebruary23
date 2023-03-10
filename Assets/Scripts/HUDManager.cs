using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : AbstractSlider
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
        HealthSlider.value = ArenaCombatManager.Instance.PlayerCurrentHealth / ArenaCombatManager.Instance.PlayerMaxHealth;
        UpdateStats();
        ArenaCombatManager.Instance.onPlayerHealthChanged += UpdateStats;
    }

    public void Dispose()
    {
        ArenaCombatManager.Instance.onPlayerHealthChanged -= UpdateStats;
    }

    public void UpdateStats()
    {
        maxHP = ArenaCombatManager.Instance.PlayerMaxHealth;
        targetHP = ArenaCombatManager.Instance.PlayerCurrentHealth < 0 ? 0 : ArenaCombatManager.Instance.PlayerCurrentHealth / maxHP;
        profileImage.sprite = ArenaCombatManager.Instance.PlayerSprite;
        NameText.text = ArenaCombatManager.Instance.PlayerName;
        LevelText.text = $"Lvl: {ArenaCombatManager.Instance.PlayerLevel.ToString()}";
        CurrentHealthText.text = ArenaCombatManager.Instance.PlayerCurrentHealth < 0 ? "0" : ArenaCombatManager.Instance.PlayerCurrentHealth.ToString();
        MaxHealthText.text = maxHP.ToString();
    }
}
