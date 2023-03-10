using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using static UnityEditor.Experimental.GraphView.GraphView;

public class CharacterManager : MonoBehaviour
{
    static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<CharacterManager>();
            return _instance;
        }
    }

    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject mainCameraPrefab;
    [SerializeField] Sprite localSprite;
    [SerializeField] AudioClip[] localHurtSounds;
    [SerializeField] AudioClip healthSound;
    [SerializeField] AudioClip defenseSound;

    private CameraController cameraController;
    [HideInInspector] public PlayerMovement player;
    public PlayerConfigSO playerConfigSO;
    public BoolSO catLevelUpSO;

    public void SetControlsEnabled(bool enabled)
    {
        Debug.Log($"SetMovementEnabled is now set to {enabled}");
        player.SetMovementEnabled(enabled);
    }

    void Awake()
    {
        player = Instantiate(playerPrefab, transform).GetComponent<PlayerMovement>();
        if (playerConfigSO.Value == null && catLevelUpSO.Value == false)
        {
            // init the playerConfig with some default values
            Debug.Log("PLAYER NOT INITIALIZED. LOADING DEFAULTS.");
            SetLevel1();
        }
        else if (playerConfigSO.Value == null && catLevelUpSO.Value == true)
        {
            Debug.Log("PLAYER NOT INITIALIZED. LOADING LEVEL 2.");
            SetLevel2();
        }
        else
        {
            // load any dependent stuff from the config SO
            Debug.Log("PLAYER IS ALREADY INITIALIZED.");
        }

        var sceneLocation = ChangeScenesManager.Instance.GetSceneLocation();
        if (sceneLocation != Vector2.zero) player.transform.position = sceneLocation;
    }

    void SetLevel1()
    {
        playerConfigSO.Value = new PlayerConfig
        {
            currentHealth = 10f,
            maxHealth = 10f,
            speed = 1f,
            level = 1,
            name = "Skully",
            sprite = localSprite,
            hurtSounds = localHurtSounds,
            abilities = new PlayerAbilities
            {
                Attacks = new Attack[2]
                            {
                                new Attack
                                {
                                    Name = "Claw",
                                    DamageAmount = 5f
                                },
                                null
                            },
                Defenses = new Defense[2]
                            {
                                new Defense
                                {
                                    Name = "Roll",
                                    DefenseAmount = 3f,
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
                                    Name = "Catnip",
                                    HealthAmount = 2f,
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
    }

    public void SetLevel2()
    {
        playerConfigSO.Value = new PlayerConfig
        {
            currentHealth = 15f,
            maxHealth = 15f,
            speed = 1f,
            level = 2,
            name = "Skully",
            sprite = localSprite,
            hurtSounds = localHurtSounds,
            abilities = new PlayerAbilities
            {
                Attacks = new Attack[2]
        {
                        new Attack
                        {
                            Name = "Claw",
                            DamageAmount = 5f
                        },
                        new Attack
                        {
                            Name = "Bite",
                            DamageAmount = 8f
                        }
        },
                Defenses = new Defense[2]
        {
                        new Defense
                        {
                            Name = "Roll",
                            DefenseAmount = 3f,
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
                            Name = "Catnip",
                            HealthAmount = 2f,
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
    }
}
