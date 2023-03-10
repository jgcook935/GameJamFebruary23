using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrannySign : MonoBehaviour, ISign
{
    [SerializeField] Sprite localSprite;
    [SerializeField] AudioClip[] localHurtSounds;
    [SerializeField] AudioClip healthSound;
    [SerializeField] AudioClip defenseSound;
    [SerializeField] BoolSO victorySO;
    private bool flippedDialogue = false;

    public List<string> text { get; set; } = new List<string>
    {
        "UGH! A stray cat!",
        "Listen here you mangey cat...",
        "You better not go near my prized petunias, they won FIRST PLACE at the state fair!",
        "Well, if you're not going to back away, I'LL MAKE YOU!",
    };

    public List<string> victoryText { get; set; } = new List<string>
    {
        "Please don't touch my petunias!",
    };

    public Action dialogCloseAction { get; set; }

    private void SetDialogCloseAction()
    {
        dialogCloseAction = () =>
        {
            // make mac an enemy here by assigning the current enemy to him and init a player config object
            CharacterManager.Instance.playerConfigSO.Value.currentEnemy = new PlayerConfig
            {
                currentHealth = 12f,
                maxHealth = 12f,
                speed = 2f,
                level = 3,
                name = "Granny",
                sprite = localSprite,
                hurtSounds = localHurtSounds,
                abilities = new PlayerAbilities
                {
                    Attacks = new Attack[2]
                        {
                        new Attack
                        {
                            Name = "Broom",
                            DamageAmount = 7
                        },
                        null
                        },
                    Defenses = new Defense[2]
                        {
                        new Defense
                        {
                            Name = "Perfume Spray",
                            DefenseAmount = 2f,
                            Sounds = new AudioClip[]
                            {
                                defenseSound
                            }
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
                            Name = "Asprin",
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
