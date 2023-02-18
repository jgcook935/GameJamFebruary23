using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class OverworldHUDManager : MonoBehaviour
{
    [SerializeField] Slider hpSlider;
    [SerializeField] Text currentHealthText;
    [SerializeField] Text maxHealthText;

    static OverworldHUDManager _instance;
    public static OverworldHUDManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<OverworldHUDManager>();
            return _instance;
        }
    }

    private void Awake()
    {
        ArenaCombatManager.Instance.onBattleStart += Init;
        ArenaCombatManager.Instance.onBattleEnd += Dispose; // not sure if i even need this...
    }

    private void Start()
    {
        UpdateStats();
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
        currentHealthText.text = ArenaCombatManager.Instance.PlayerCurrentHealth < 0 ? "0" : ArenaCombatManager.Instance.PlayerCurrentHealth.ToString();
        maxHealthText.text = ArenaCombatManager.Instance.PlayerMaxHealth.ToString();
        hpSlider.value = ArenaCombatManager.Instance.PlayerCurrentHealth < 0 ? 0 : ArenaCombatManager.Instance.PlayerCurrentHealth / 10f;
    }
}
