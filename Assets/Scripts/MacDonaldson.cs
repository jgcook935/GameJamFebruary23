using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class MacDonaldson : MonoBehaviour, ISign
{
    [SerializeField] Sprite localSprite;
    [SerializeField] AudioClip[] localHurtSounds;
    [SerializeField] AudioClip healthSound;
    [SerializeField] AudioClip[] defenseSounds;
    [SerializeField] BoolSO victorySO;
    private bool flippedDialogue = false;

    public List<string> text { get; set; } = new List<string>
    {
        "Hey there, kitty! What are you doing out here all alone? This town isn't most friendly place around.",
        "Let me give you a little tip...",
        "To progress through the town and discover new areas, you'll need to pass through the gates.",
        "To remove the gates, you'll need to win a battle.",
        "Oh, and one more thing...",
        "If you insist on living outdoors all alone, you're gonna need some practice. Let's spar!"
    };

    public List<string> victoryText { get; set; } = new List<string>
    {
        "You're one tough kitty,",
        "If your health gets low, you can eat some cat food from the bowls the neighbors leave out. Good luck!"
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
                name = "Mac Donaldson",
                sprite = localSprite,
                hurtSounds = localHurtSounds,
                abilities = new PlayerAbilities
                {
                    Attacks = new Attack[2]
                        {
                        new Attack
                        {
                            Name = "Spray Bottle",
                            DamageAmount = 5f
                        },
                        null
                        },
                    Defenses = new Defense[2]
                        {
                        new Defense
                        {
                            Name = "Pants Hike",
                            DefenseAmount = 2f,
                            Sounds = defenseSounds
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
                            Name = "Cuppa Joe",
                            HealthAmount = 3f,
                            Sound = healthSound
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

        dialogCloseAction += () => StartCoroutine(UIManager.Instance.TransitionToArena(victorySO));
    }

    private void SetVictoryDialogCloseAction()
    {
        dialogCloseAction = () => { };
    }

    void Awake()
    {
        SetDialogCloseAction();
    }

    void Update()
    {
        if (victorySO.Value && !flippedDialogue)
        {
            text = victoryText;
            SetVictoryDialogCloseAction();
            flippedDialogue = true;
        }
    }
}
