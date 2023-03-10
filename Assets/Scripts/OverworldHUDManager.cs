using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class OverworldHUDManager : AbstractSlider
{
    [SerializeField] Slider hpSlider;
    [SerializeField] Text currentHealthText;
    [SerializeField] Text maxHealthText;
    [SerializeField] GameObject healthFullMessage;
    [SerializeField] Image hpColorImage;

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

    private void Update()
    {
        UpdateSlider(hpSlider, hpColorImage);
    }

    public void Init()
    {
        hpSlider.value = ArenaCombatManager.Instance.PlayerCurrentHealth / ArenaCombatManager.Instance.PlayerMaxHealth;
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
        currentHealthText.text = ArenaCombatManager.Instance.PlayerCurrentHealth < 0 ? "0" : ArenaCombatManager.Instance.PlayerCurrentHealth.ToString();
        maxHealthText.text = maxHP.ToString();
    }

    public IEnumerator ShowHealthFullMessage()
    {
        healthFullMessage.SetActive(true);
        yield return new WaitForSeconds(2);
        healthFullMessage.SetActive(false);
        yield return null;
    }
}
