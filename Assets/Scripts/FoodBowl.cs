using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FoodBowl : MonoBehaviour, ISign
{
    const float healthBoost = 10f;

    public List<string> text { get; set; } = new List<string>
    {
        "Finally, some food.",
    };

    public Action dialogCloseAction { get; set; }
    public Action onFoodEatenAction;
    //private PlayerConfig player;

    void Awake()
    {
        dialogCloseAction += HealPlayer;
    }

    private void HealPlayer()
    {
        if (CharacterManager.Instance.playerConfigSO.Value.currentHealth == CharacterManager.Instance.playerConfigSO.Value.maxHealth)
        {
            Debug.Log("PLAYER IS ALREADY FULL OF HEALTH. DON'T BE GREEDY.");
        }
        else if (CharacterManager.Instance.playerConfigSO.Value.currentHealth + healthBoost >= CharacterManager.Instance.playerConfigSO.Value.maxHealth)
        {
            Debug.Log("PLAYER HEALTH PLUS HEALTH BOOST IS MAX HEALTH, JUST MAXING OUT.");
            CharacterManager.Instance.playerConfigSO.Value.currentHealth = CharacterManager.Instance.playerConfigSO.Value.maxHealth;
            // play a chomp sound
            onFoodEatenAction?.Invoke();
            OverworldHUDManager.Instance.UpdateStats();
            GameObject.Destroy(this.gameObject, 2f);
        }
        else
        {
            Debug.Log("PLAYER HEALTH PLUS HEALTH BOOST LESS THAN MAX, JUST ADDING IT.");
            CharacterManager.Instance.playerConfigSO.Value.currentHealth += healthBoost;
            // play a chomp sound
            onFoodEatenAction?.Invoke();
            OverworldHUDManager.Instance.UpdateStats();
            GameObject.Destroy(this.gameObject, 2f);
        }
    }

    public void SetOnFoodEatenAction(Action action)
    {
        onFoodEatenAction += action;
    }
}
