using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacoonSign : MonoBehaviour, ISign
{
    [SerializeField] Sprite localSprite;
    [SerializeField] AudioClip[] localHurtSounds;
    [SerializeField] AudioClip healthSound;
    [SerializeField] AudioClip[] defenseSounds;
    [SerializeField] BoolSO victorySO;
    private bool flippedDialogue = false;

    public List<string> text { get; set; } = new List<string>
    {
        "Well, well, well...",
        "Look at the fancy house cat",
        "Run on home to your family, house cat. You can't survive on these streets.",
        "Oh you want to show me what you got?",
    };

    public List<string> victoryText { get; set; } = new List<string>
    {
        "Please don't hurt me!",
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
                            DamageAmount = 6f
                        },
                        null
                        },
                    Defenses = new Defense[2]
                        {
                        new Defense
                        {
                            Name = "Trash Dive",
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
                            Name = "Eat Rotten BigMac",
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
