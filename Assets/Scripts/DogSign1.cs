using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSign1 : MonoBehaviour, ISign
{
    [SerializeField] Sprite localSprite;
    [SerializeField] AudioClip[] localHurtSounds;
    [SerializeField] AudioClip healthSound;
    [SerializeField] AudioClip defenseSound;
    [SerializeField] BoolSO victorySO;

    public List<string> text { get; set; } = new List<string>
    {
        "Hey there, puss!",
        "It's getting pretty crowded around here with the both of us don't ya think?!",
        "The humans haven't even played with you once since they brought me home.",
        "I get to sleep in the bed and I get all the best treats, you just get ignored.",
        "I think its time you left and didn't come back, the humans won't even notice!",
        "I'll even show you the way out!"
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
                speed = 2f,
                level = 2,
                name = "Dog",
                sprite = localSprite,
                hurtSounds = localHurtSounds,
                abilities = new PlayerAbilities
                {
                    Attacks = new Attack[2]
                        {
                        new Attack
                        {
                            Name = "Bite",
                            DamageAmount = 10
                        },
                        null
                        },
                    Defenses = new Defense[2]
                        {
                        new Defense
                        {
                            Name = "Cower",
                            DefenseAmount = 1,
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
                    HealthBoosts = new HealthBoost[]
                        {
                        new HealthBoost
                        {
                            Name = "Chew Bone",
                            HealthAmount = 9f,
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

    void Awake()
    {
        SetDialogCloseAction();
    }
}
