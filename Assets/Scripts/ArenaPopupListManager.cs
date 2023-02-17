using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArenaPopupListManager : MonoBehaviour
{
    [SerializeField] ArenaCombatManager CombatManager;
    [SerializeField] GameObject ButtonPrefab;
    [SerializeField] GameObject PlayerControls;

    private List<GameObject> buttons = new List<GameObject>();

    public void ClearButtonsOnAction()
    {
        buttons.ForEach(x => GameObject.Destroy(x));
        buttons.Clear();
    }

    private void HideOnAction()
    {
        PlayerControls.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void ShowAttacks()
    {
        foreach(var attack in CombatManager.PlayerAbilities.Attacks)
        {
            if (attack == null) continue;
            var button = GameObject.Instantiate(ButtonPrefab, transform);
            button.GetComponentInChildren<TMP_Text>().text = attack.Name;
            button.GetComponent<Button>().onClick.AddListener(() => CombatManager.DamageEnemy(attack.DamageAmount));
            button.GetComponent<Button>().onClick.AddListener(() => this.ClearButtonsOnAction());
            button.GetComponent<Button>().onClick.AddListener(() => this.HideOnAction());
            buttons.Add(button);
        }
    }

    public void ShowDefenses()
    {
        foreach (var defense in CombatManager.PlayerAbilities.Defenses)
        {
            if (defense == null) continue;
            var button = GameObject.Instantiate(ButtonPrefab, transform);
            button.GetComponentInChildren<TMP_Text>().text = defense.Name;
            button.GetComponent<Button>().onClick.AddListener(() => CombatManager.SetPlayerDefense(defense.DefenseAmount));
            button.GetComponent<Button>().onClick.AddListener(() => this.ClearButtonsOnAction());
            button.GetComponent<Button>().onClick.AddListener(() => this.HideOnAction());
            buttons.Add(button);
        }
    }

    public void ShowItems()
    {
        foreach (var healthBoost in CombatManager.PlayerInventory.HealthBoosts)
        {
            if (healthBoost == null) continue;
            var button = GameObject.Instantiate(ButtonPrefab, transform);
            button.GetComponentInChildren<TMP_Text>().text = healthBoost.Name;
            button.GetComponent<Button>().onClick.AddListener(() => CombatManager.HealPlayer(healthBoost.HealthAmount));
            button.GetComponent<Button>().onClick.AddListener(() => this.ClearButtonsOnAction());
            button.GetComponent<Button>().onClick.AddListener(() => this.HideOnAction());
            buttons.Add(button);
        }

        foreach (var weapon in CombatManager.PlayerInventory.Weapons)
        {
            if (weapon == null) continue;
            var button = GameObject.Instantiate(ButtonPrefab, transform);
            button.GetComponentInChildren<TMP_Text>().text = weapon.Name;
            buttons.Add(button);
        }
    }
}
