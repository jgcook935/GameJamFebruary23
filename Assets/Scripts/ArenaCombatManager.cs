using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class DefenseConfig
{
    public bool isEnabled { get; set; }
    public float defenseAmount { get; set; }
}

public class ArenaCombatManager : MonoBehaviour
{
    [SerializeField] PlayerConfigSO playerConfig;
    [SerializeField] GameObject resultText;
    [SerializeField] GameObject playerControls;

    public Action onEnemyDamaged;
    public Action onPlayerDamaged;

    private DefenseConfig playerDefenseConfig;
    private DefenseConfig enemyDefenseConfig;

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

    void Awake()
    {
        if (PlayerSpeed >= EnemySpeed)
        {
            PassToPlayer();
        }
        else
        {
            PassToEnemy();
        }
    }

    public void DamagePlayer(float damage)
    {
        var actualDamage = damage;
        if (playerDefenseConfig != null && playerDefenseConfig.isEnabled)
        {
            actualDamage = playerDefenseConfig.defenseAmount >= damage ? 0 : damage - playerDefenseConfig.defenseAmount;
            playerDefenseConfig = null;
        }
        // use the damage amount to lower the player's current health
        playerConfig.Value.currentHealth -= actualDamage;

        // update player stats on screen
        onPlayerDamaged?.Invoke();

        var message = "";
        if (actualDamage == 0)
        {
            message = $"UMM, OKAY... You weren't even phased by that attack.";
        }
        else if (actualDamage > 0 && actualDamage < 5)
        {
            message = $"THAT'S GONNA HURT IN THE MORNING! You took {actualDamage} damage.";
        }
        else if (actualDamage >= 5)
        {
            message = $"THEY'RE SO STRONG! YOU HAVE BEEN DAMAGED BY {actualDamage} HEALTH POINTS!!!";
        }

        // show a message to the player that they hit the enemy
        Debug.Log(message);

        // do a check to see if they're defeated or not
        if (playerConfig.Value.currentHealth <= 0)
        {
            Debug.Log($"Shucks, you've been defeated. Better luck next time.");            
            StartCoroutine(OnDefeat());
        }
        else
        {
            StartCoroutine(PlayerWaitBeforeAction());
            TogglePlayerControls(true);
        }
    }

    public void DamageEnemy(float damage)
    {
        var actualDamage = damage;
        if (enemyDefenseConfig != null && enemyDefenseConfig.isEnabled)
        {
            actualDamage = enemyDefenseConfig.defenseAmount >= damage ? 0 : damage - enemyDefenseConfig.defenseAmount;
            enemyDefenseConfig = null;
        }
        // use the damage amount to lower the enemy's current health
        playerConfig.Value.currentEnemy.currentHealth -= actualDamage;

        // update enemy stats on screen
        onEnemyDamaged?.Invoke();

        var message = "";
        if (actualDamage == 0)
        {
            message = $"WOW, THAT DIDN'T DO MUCH... {EnemyName} wasn't even phased by that attack.";
        }
        else if (actualDamage > 0 && actualDamage < 5)
        {
            message = $"DANG, WHAT A HIT! {EnemyName} took {actualDamage} damage!";
        }
        else if (actualDamage >= 5)
        {
            message = $"SWEET ATTACK! {EnemyName} HAS BEEN DAMAGED BY {actualDamage} health points!!!";
        }

        // show a message to the player that they hit the enemy
        Debug.Log(message);

        // do a check to see if they're defeated or not
        if (playerConfig.Value.currentEnemy.currentHealth <= 0)
        {
            Debug.Log($"{EnemyName} has been defeated!");
            StartCoroutine(OnVictory());
        }
        else
        {
            PassToEnemy();
        }
    }

    public void HealPlayer(float health)
    {
        if (playerConfig.Value.currentHealth + health > playerConfig.Value.maxHealth)
        {
            playerConfig.Value.currentHealth = playerConfig.Value.maxHealth;
        }
        else
        {
            playerConfig.Value.currentHealth += health;
        }
        Debug.Log($"PLAYER HEALED BY {health} hit points");
        PassToEnemy();
    }

    public void HealEnemy(float health)
    {
        if (playerConfig.Value.currentEnemy.currentHealth + health > playerConfig.Value.currentEnemy.maxHealth)
        {
            playerConfig.Value.currentEnemy.currentHealth = playerConfig.Value.currentEnemy.maxHealth;
        }
        else
        {
            playerConfig.Value.currentEnemy.currentHealth += health;
        }
        Debug.Log($"ENEMY HEALED BY {health} hit points");
    }

    public void SetPlayerDefense(float defense)
    {
        playerDefenseConfig = new DefenseConfig
        {
            isEnabled = true,
            defenseAmount = defense
        };
        Debug.Log($"PLAYER SHIELDED WITH {defense} hit points");
        PassToEnemy();
    }

    public void SetEnemyDefense(float defense)
    {
        enemyDefenseConfig = new DefenseConfig
        {
            isEnabled = true,
            defenseAmount = defense
        };
        Debug.Log($"ENEMY SHIELDED WITH {defense} hit points");
        PassToPlayer();
    }

    IEnumerator PlayerWaitBeforeAction()
    {
        yield return new WaitForSeconds(2);
    }

    IEnumerator OnEnemyTurn()
    {
        Debug.Log("ENEMY'S TURN TO ATTACK...");
        yield return new WaitForSeconds(2);
        var actionChooser = UnityEngine.Random.Range(0, 31);
        if (actionChooser <= 10)
        {
            Debug.Log($"THE ACTION CHOOSER CHOSE TO DAMAGE BECAUSE THE VALUE WAS {actionChooser}");
            DamagePlayer(playerConfig.Value.currentEnemy.abilities.Attacks[0].DamageAmount);
        }
        else if (actionChooser <= 20)
        {
            Debug.Log($"THE ACTION CHOOSER CHOSE TO DEFEND BECAUSE THE VALUE WAS {actionChooser}");
            SetEnemyDefense(playerConfig.Value.currentEnemy.abilities.Defenses[0].DefenseAmount);
        }
        else
        {
            Debug.Log($"THE ACTION CHOOSER CHOSE TO HEAL BECAUSE THE VALUE WAS {actionChooser}");
            HealEnemy(playerConfig.Value.currentEnemy.inventory.HealthBoosts[0].HealthAmount);
        }
        yield return null;
    }

    IEnumerator OnVictory()
    {
        var text = resultText.GetComponent<TMP_Text>();
        text.text = "VICTORY";
        text.color = Color.green;
        resultText.gameObject.SetActive(true);
        TogglePlayerControls(false);
        // play some sort of victory sound
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(ChangeScenesManager.Instance.previousSceneIndex);
    }

    IEnumerator OnDefeat()
    {
        var text = resultText.GetComponent<TMP_Text>();
        text.text = "DEFEAT";
        text.color = Color.red;
        resultText.gameObject.SetActive(true);
        TogglePlayerControls(false);
        // play some sort of defeat sound
        yield return new WaitForSeconds(3);
        playerConfig.Value.currentHealth = 1f; // give them a little health to get to the next fight or find some health
        SceneManager.LoadScene(ChangeScenesManager.Instance.previousSceneIndex);
    }

    private void PassToEnemy()
    {
        TogglePlayerControls(false);
        StartCoroutine(OnEnemyTurn());
    }

    private void PassToPlayer()
    {
        StartCoroutine(PlayerWaitBeforeAction());
        TogglePlayerControls(true);
    }

    private void TogglePlayerControls(bool isActive)
    {
        foreach (Transform child in playerControls.transform)
        {
            child.gameObject.SetActive(isActive);
        }
    }
}
