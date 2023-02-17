using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacoonSign : MonoBehaviour, ISign
{
    public List<string> text { get; set; } = new List<string>
    {
        "I'm a racoon",
        "Let's battle",
    };

    public Action dialogCloseAction { get; set; } = () =>
    {
        // make mac an enemy here by assigning the current enemy to him and init a player config object
        CharacterManager.Instance.playerConfigSO.Value.currentEnemy = new PlayerConfig
        {
            currentHealth = 10f,
            maxHealth = 10f,
            speed = 1f,
            level = 1,
            name = "Racoon",
            abilities = new PlayerAbilities
            {
                Attacks = new Attack[2]
                    {
                        new Attack
                        {
                            Name = "Rabies Bite",
                            DamageAmount = 9
                        },
                        null
                    },
                Defenses = new Defense[2]
                    {
                        new Defense
                        {
                            Name = "Cower",
                            DefenseAmount = 1
                        },
                        null
                    }
            },
            inventory = new PlayerInventory
            {
                HealthBoosts = new HealthBoost[2]
                    {
                        new HealthBoost
                        {
                            Name = "Eat Rotten BigMac",
                            HealthAmount = 3f
                        },
                        null
                    },
                Weapons = new Weapon[2]
                    {
                        null,
                        null
                    }
            }
        };
    };

    void Awake()
    {
        dialogCloseAction += () => StartCoroutine(UIManager.Instance.TransitionToArena());
    }
}
