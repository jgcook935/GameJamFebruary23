using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class MacDonaldson : MonoBehaviour, ISign
{
    public List<string> text { get; set; } = new List<string>
    {
        "Shoo, kitty",
        "...",
        "I said SHOOO KITTY"
    };

    public Action dialogCloseAction { get; set; } = () =>
    {
        // make mac an enemy here by assigning the current enemy to him and init a player config object
        CharacterManager.Instance.playerConfigSO.Value.currentEnemy = new PlayerConfig
        {
            currentHealth = 5f,
            maxHealth = 5f,
            speed = 1f,
            level = 1,
            name = "Mac Donaldson",
            abilities = new PlayerAbilities
            {
                Attacks = new Attack[2]
                    {
                        new Attack("Spray Bottle", 2),
                        null
                    },
                Defenses = new Defense[2]
                    {
                        new Defense("Cower", 1),
                        null
                    }
            },
            inventory = new PlayerInventory
            {
                HealthBoosts = new HealthBoost[2]
                    {
                        null,
                        null
                    },
                Weapons = new Weapon[2]
                    {
                        null,
                        null
                    }
            }
        };

        ChangeScenesManager.Instance.LoadScene(CharacterManager.Instance.player.transform.position, 3); // 3 is the arena scene
    };
}
