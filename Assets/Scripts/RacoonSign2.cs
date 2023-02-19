using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacoonSign2 : MonoBehaviour, ISign
{
    [SerializeField] Sprite localSprite;
    [SerializeField] AudioClip[] localHurtSounds;
    [SerializeField] AudioClip healthSound;
    [SerializeField] AudioClip[] defenseSounds;
    [SerializeField] BoolSO victorySO;
    private bool flippedDialogue = false;


    public List<string> text { get; set; } = new List<string>
    {
        "..? Oh it's you housecat",
        "You got the best of me last time but you got lucky",
        "Time for some payback",
    };

    public List<string> victoryText { get; set; } = new List<string>
    {
        "Alright, alright, you're a bad cat",
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
                maxHealth = 12f,
                speed = 1f,
                level = 2,
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
