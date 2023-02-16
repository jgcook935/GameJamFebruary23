using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaCombatManager : MonoBehaviour
{
    [SerializeField] PlayerConfigSO playerConfig;

    #region expose player stuff

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

    #endregion

    #region expose enemy stuff

    public float EnemyCurrentHealth
    {
        get { return playerConfig.Value.currentEnemy.currentHealth; }
    }

    public float EnemyMaxHealth
    {
        get { return playerConfig.Value.currentEnemy.maxHealth; }
    }

    public float EnemySpeed
    {
        get { return playerConfig.Value.currentEnemy.speed; }
    }

    public int EnemyLevel
    {
        get { return playerConfig.Value.currentEnemy.level; }
    }

    public string EnemyName
    {
        get { return playerConfig.Value.currentEnemy.name; }
    }

    public PlayerAbilities EnemyAbilities
    {
        get { return playerConfig.Value.currentEnemy.abilities; }
    }

    public PlayerInventory EnemyInventory
    {
        get { return playerConfig.Value.currentEnemy.inventory; }
    }

    #endregion
}
