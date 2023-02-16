using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaCombatManager : MonoBehaviour
{
    [SerializeField] PlayerConfigSO playerConfig;

    public float PlayerCurrentHealth
    {
        get { return playerConfig.Value.currentHealth; }
    }

    public float PlayerMaxHealth
    {
        get { return playerConfig.Value.maxHealth; }
    }

    public float PlayerSpeed
    {
        get { return playerConfig.Value.speed; }
    }

    public int PlayerLevel
    {
        get { return playerConfig.Value.level; }
    }

    public string PlayerName
    {
        get { return playerConfig.Value.name; }
    }

    public PlayerAbilities PlayerAbilities
    {
        get { return playerConfig.Value.abilities; }
    }

    public PlayerInventory PlayerInventory
    {
        get { return playerConfig.Value.inventory; }
    }
}
