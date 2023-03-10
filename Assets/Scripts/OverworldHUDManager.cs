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
        hpSlider.value = ArenaCombatManager.Instance.PlayerCurrentHealth < 0 ? 0 : ArenaCombatManager.Instance.PlayerCurrentHealth / ArenaCombatManager.Instance.PlayerMaxHealth;
        if(ArenaCombatManager.Instance.PlayerCurrentHealth < 0.5f * ArenaCombatManager.Instance.PlayerMaxHealth)
        {
            hpColorImage.color = new Color(1, 0, 0, 150f/255f);
        } 
        else hpColorImage.color = new Color(29f / 255f, 168f / 255f, 6f / 255f, 150f / 255f);
    }

    public IEnumerator ShowHealthFullMessage()
    {
        healthFullMessage.SetActive(true);
        yield return new WaitForSeconds(2);
        healthFullMessage.SetActive(false);
        yield return null;
    }
}
