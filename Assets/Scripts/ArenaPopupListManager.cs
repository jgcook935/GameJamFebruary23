using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArenaPopupListManager : MonoBehaviour
{
    [SerializeField] ArenaCombatManager CombatManager;
    [SerializeField] GameObject ButtonPrefab;

    private List<GameObject> buttons = new List<GameObject>();

    public void OnCancel()
    {
        buttons.ForEach(x => GameObject.Destroy(x));
        buttons.Clear();
    }

    public void ShowAttacks()
    {
        foreach(var attack in CombatManager.PlayerAbilities.Attacks)
        {
            if (attack == null) continue;
            var button = GameObject.Instantiate(ButtonPrefab, transform);
            button.GetComponentInChildren<TMP_Text>().text = attack.Name;
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
