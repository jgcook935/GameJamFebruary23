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

    public List<string> text { get; set; } = new List<string>
    {
        "Hey there, kitty! What are you doing out here all alone in this small town? It's not the most friendly place around",
        "...",
        "I know what you mean. I'm a law-abiding, tax-paying citizen, but people here still give me the cold shoulder. Here, I've got an extra donut. Take it. ",
        "What?! You don't like donuts? How dare you!? You should be grateful for what I'm offering you! ",
        "Fine, then! If you won't accept my hospitality, then I'm not going to be so friendly anymore. Prepare to battle, kitty!"
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
                            DamageAmount = 7f
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

        dialogCloseAction += () => StartCoroutine(UIManager.Instance.TransitionToArena());
    }

    void Awake()
    {
        SetDialogCloseAction();
    }
}
