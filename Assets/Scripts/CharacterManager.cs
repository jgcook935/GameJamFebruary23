using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    [SerializeField] PlayerConfigSO playerConfigSO;

    private CameraController cameraController;
    [HideInInspector] public PlayerMovement player;

    public void SetControlsEnabled(bool enabled)
    {
        player.SetMovementEnabled(enabled);
    }

    void Awake()
    {
        player = Instantiate(playerPrefab, transform).GetComponent<PlayerMovement>();
        if (playerConfigSO.Value == null)
        {
            // init the playerConfig with some default values
            Debug.Log("PLAYER NOT INITIALIZED. LOADING DEFAULTS.");
            playerConfigSO.Value = new PlayerConfig
            {
                currentHealth = 10f,
                maxHealth = 10f,
                speed = 1f,
                level = 1,
                name = "Skully",
                abilities = new PlayerAbilities
                {
                    attacks = new Attack[2]
                    {
                        new Attack("Claw", 5),
                        null
                    },
                    defenses = new Defense[2]
                    {
                        new Defense("Roll", 3),
                        null
                    }
                },
                inventory = new PlayerInventory
                {
                    healthBoosts = new HealthBoost[2]
                    {
                        null,
                        null
                    },
                    weapons = new Weapon[2]
                    {
                        null,
                        null
                    }
                }
            };
        }
        else
        {
            // load any dependent stuff from the config SO
            Debug.Log("PLAYER IS ALREADY INITIALIZED.");
        }

        var sceneLocation = ChangeScenesManager.Instance.GetSceneLocation();
        if (sceneLocation != Vector2.zero) player.transform.position = sceneLocation;
    }
}
