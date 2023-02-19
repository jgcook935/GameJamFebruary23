using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacoonSign : MonoBehaviour, ISign
{
    [SerializeField] Sprite localSprite;
    [SerializeField] AudioClip[] localHurtSounds;

    public List<string> text { get; set; } = new List<string>
    {
        "Well, well, well...",
        "Look at the fancy housecat",
        "Run on home to your family, housecat",
        "Oh you want to show me what you got?",
    };

    public Action dialogCloseAction { get; set; }

    private void SetDialogCloseAction()
    {
        dialogCloseAction = () =>
        {

            // make mac an enemy here by assigning the current enemy to him and init a player config object
            CharacterManager.Instance.playerConfigSO.Value.currentEnemy = new PlayerConfig
            {
                currentHealth = 10f,
                maxHealth = 10f,
                speed = 1f,
                level = 1,
                name = "Racoon",
                sprite = localSprite,
                hurtSounds = localHurtSounds,
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

        dialogCloseAction += () => StartCoroutine(UIManager.Instance.TransitionToArena());
    }

    void Awake()
    {
        SetDialogCloseAction();
    }
}
