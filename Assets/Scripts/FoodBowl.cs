using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FoodBowl : MonoBehaviour, ISign
{
    const float healthBoost = 10f;

    public List<string> text { get; set; } = new List<string>
    {
        "Ah, some food.",
    };

    public Action dialogCloseAction { get; set; }
    public Action onFoodEatenAction;
    private AudioSource source => GetComponent<AudioSource>();
    [SerializeField] AudioClip bite;

    void Awake()
    {
        dialogCloseAction += HealPlayer;
    }

    private void HealPlayer()
    {
        if (CharacterManager.Instance.playerConfigSO.Value.currentHealth == CharacterManager.Instance.playerConfigSO.Value.maxHealth)
        {
            Debug.Log("PLAYER IS ALREADY FULL OF HEALTH. DON'T BE GREEDY.");
            StartCoroutine(OverworldHUDManager.Instance.ShowHealthFullMessage());
        }
        else if (CharacterManager.Instance.playerConfigSO.Value.currentHealth + healthBoost >= CharacterManager.Instance.playerConfigSO.Value.maxHealth)
        {
            Debug.Log("PLAYER HEALTH PLUS HEALTH BOOST IS MAX HEALTH, JUST MAXING OUT.");
            CharacterManager.Instance.playerConfigSO.Value.currentHealth = CharacterManager.Instance.playerConfigSO.Value.maxHealth;
            source.PlayOneShot(bite);
            OverworldHUDManager.Instance.UpdateStats();
        }
        else
        {
            Debug.Log("PLAYER HEALTH PLUS HEALTH BOOST LESS THAN MAX, JUST ADDING IT.");
            CharacterManager.Instance.playerConfigSO.Value.currentHealth += healthBoost;
            source.PlayOneShot(bite);
            OverworldHUDManager.Instance.UpdateStats();
        }
    }

    public void SetOnFoodEatenAction(Action action)
    {
        onFoodEatenAction += action;
    }
}
