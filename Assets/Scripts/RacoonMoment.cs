using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RacoonMoment : MonoBehaviour, ISign
{
    public List<string> text { get; set; } = new List<string>
    {
        "I'm a racoon",
        "Let's battle",
    };

    [SerializeField] private Animator exclamationAnimator;
    [SerializeField] private SpriteRenderer exclamationSprite;
    [SerializeField] private AIPath aipath;
    [SerializeField] private BoolSO foughtRacoon;

    private bool startedDialogue = false;

    private AIDestinationSetter destinationSetter;

    void Start()
    {
        destinationSetter = GetComponentInParent<AIDestinationSetter>();
    }

    void Update()
    {
        if (destinationSetter.target == null) destinationSetter.target = CharacterManager.Instance.player.transform;
        if (aipath.reachedEndOfPath && !startedDialogue)
        {
            startedDialogue = true;
            GetComponent<Sign>().Click();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !foughtRacoon.Value)
        {
            foughtRacoon.Value = true;
            CharacterManager.Instance.player.Enabled = false;
            StartCoroutine(Exclamation());
        }
    }

    public Action dialogCloseAction { get; set; } = () =>
    {
        // make mac an enemy here by assigning the current enemy to him and init a player config object
        CharacterManager.Instance.playerConfigSO.Value.currentEnemy = new PlayerConfig
        {
            currentHealth = 10f,
            maxHealth = 10f,
            speed = 1f,
            level = 1,
            name = "Mac Donaldson",
            abilities = new PlayerAbilities
            {
                Attacks = new Attack[2]
                    {
                        new Attack
                        {
                            Name = "Spray Bottle",
                            DamageAmount = 10
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
                            Name = "Cuppa Joe",
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

        ChangeScenesManager.Instance.LoadScene(CharacterManager.Instance.player.transform.position, 3); // 3 is the arena scene
    };

    IEnumerator Exclamation()
    {
        exclamationSprite.enabled = true;
        exclamationAnimator.enabled = true;
        exclamationAnimator.Play("PopupEmote");
        yield return new WaitForSeconds(1);
        exclamationAnimator.Play("CloseEmote");
        yield return new WaitForSeconds(1);
        destinationSetter.enabled = true;
    }
}
