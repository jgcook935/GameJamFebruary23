using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[CreateAssetMenu]
public class PlayerConfigSO : ScriptableObject
{
    [SerializeField]
    private PlayerConfig _value;
    private PlayerConfig tempValue;

    public PlayerConfig Value
    {
        get { return tempValue; }
        set { tempValue = value; }
    }

    private void OnEnable()
    {
        Value = _value;
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    // You can also use OnAfterDeserialize for the other way around
    public void OnAfterDeserialize()
    {
    }
}

/// <summary>
/// This will be used to SO and will be a combination of classes to define the player's game status.
/// </summary>
public class PlayerConfig
{
    public float currentHealth { get; set; }
    public float maxHealth { get; set; }
    public float speed { get; set; }
    public int level { get; set; }
    public string name { get; set; }
    public PlayerAbilities abilities { get; set; }
    public PlayerInventory inventory { get; set; }
    // should this also have a field for current enemy? that way i wouldn't need SOs for each enemy, but could just
    // pass it in when you accept a challenge, and clear it when you either win/lose/run...
    // public Enemy currentEnemy { get; set; }
}

/// <summary>
/// Simple inventory to hold HealthBoosts and Weapons.
/// </summary>
public class PlayerInventory
{
    // healthboosts and weapons are limited to 3 items so players will have to drop one to make room
    public HealthBoost[] healthBoosts { get; set; } = new HealthBoost[2];
    public Weapon[] weapons { get; set; } = new Weapon[2];
}

/// <summary>
/// A positive item you'd find on the map and use to heal in combat.
/// </summary>
public abstract class HealthBoost
{
    public string name { get; set; }
    public int healthAmount { get; set; }
}

/// <summary>
/// A negative item you'd find on the map and use to damage an enemy in combat.
/// </summary>
public abstract class Weapon
{
    public string name { get; set; }
    public int damageAmount { get; set; }
}

/// <summary>
/// A simple ability inventory to hold Attack and Defensive moves.
/// </summary>
public class PlayerAbilities
{
    // attacks and defenses are limited to 3 items so players will have to drop one to make room
    public Attack[] attacks { get; set; } = new Attack[2];
    public Defense[] defenses { get; set; } = new Defense[2];
}

/// <summary>
/// An attack move a player can use against an enemy in combat.
/// </summary>
public class Attack
{
    public Attack(string name, int damageAmount)
    {
        Name = name;
        DamageAmount = damageAmount;
    }

    public string Name { get; set; }
    public int DamageAmount { get; set; }
}

/// <summary>
/// A defensive move a player can use against an enemy in combat.
/// </summary>
public class Defense
{
    public Defense(string name, int defenseAmount)
    {
        Name = name;
        DefenseAmount = defenseAmount;
    }

    public string Name { get; set; }
    public int DefenseAmount { get; set; }
}