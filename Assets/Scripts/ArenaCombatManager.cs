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
    [SerializeField] GameObject actionText;
    [SerializeField] GameObject playerControls;

    public Action onEnemyHealthChanged;
    public Action onPlayerHealthChanged;

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
            TogglePlayerControls(false);
            PassToEnemy();
        }
    }

    public void DamagePlayer(Attack attack)
    {
        var actualDamage = attack.DamageAmount;
        if (playerDefenseConfig != null && playerDefenseConfig.isEnabled)
        {
            actualDamage = playerDefenseConfig.defenseAmount >= attack.DamageAmount ? 0 : attack.DamageAmount - playerDefenseConfig.defenseAmount;
            playerDefenseConfig = null;
        }
        // use the damage amount to lower the player's current health
        playerConfig.Value.currentHealth -= actualDamage;

        // update player stats on screen
        onPlayerHealthChanged?.Invoke();

        var message = "";
        if (actualDamage == 0)
        {
            message = $"{EnemyName} used {attack.Name}. It wasn't very effective.";
        }
        else if (actualDamage > 0 && actualDamage < 5)
        {
            message = $"{EnemyName} used {attack.Name}. You took {actualDamage} damage.";
        }
        else if (actualDamage >= 5)
        {
            message = $"{EnemyName} used {attack.Name}. OOF - You took {actualDamage} damage.";
        }

        // show a message to the player that they hit the enemy
        Debug.Log(message);

        Action followAction = () =>
        {
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
        };

        StartCoroutine(ShowActionText(message, Color.black, 3, followAction));
    }

    public void DamageEnemy(Attack attack)
    {
        var actualDamage = attack.DamageAmount;
        if (enemyDefenseConfig != null && enemyDefenseConfig.isEnabled)
        {
            actualDamage = enemyDefenseConfig.defenseAmount >= attack.DamageAmount ? 0 : attack.DamageAmount - enemyDefenseConfig.defenseAmount;
            enemyDefenseConfig = null;
        }
        // use the damage amount to lower the enemy's current health
        playerConfig.Value.currentEnemy.currentHealth -= actualDamage;

        // update enemy stats on screen
        onEnemyHealthChanged?.Invoke();

        var message = "";
        if (actualDamage == 0)
        {
            message = $"{PlayerName} used {attack.Name}. {EnemyName} wasn't even phased.";
        }
        else if (actualDamage > 0 && actualDamage < 5)
        {
            message = $"{PlayerName} used {attack.Name}. {EnemyName} took {actualDamage} damage. What a hit!";
        }
        else if (actualDamage >= 5)
        {
            message = $"{PlayerName} used {attack.Name}. {EnemyName} took {actualDamage} damage. They're shook!";
        }

        // show a message to the player that they hit the enemy
        Debug.Log(message);

        TogglePlayerControls(false);

        Action followAction = () =>
        {
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
        };

        StartCoroutine(ShowActionText(message, Color.black, 3, followAction));
    }

    public void HealPlayer(HealthBoost healthBoost)
    {
        if (playerConfig.Value.currentHealth + healthBoost.HealthAmount > playerConfig.Value.maxHealth)
        {
            playerConfig.Value.currentHealth = playerConfig.Value.maxHealth;
        }
        else
        {
            playerConfig.Value.currentHealth += healthBoost.HealthAmount;
        }
        onPlayerHealthChanged?.Invoke();
        var message = $"{PlayerName} used {healthBoost.Name} to heal for {healthBoost.HealthAmount} health points.";
        Debug.Log(message);

        TogglePlayerControls(false);

        Action followAction = () =>
        {
            PassToEnemy();
        };

        StartCoroutine(ShowActionText(message, Color.black, 3, followAction));
    }

    public void HealEnemy(HealthBoost healthBoost)
    {
        if (playerConfig.Value.currentEnemy.currentHealth + healthBoost.HealthAmount > playerConfig.Value.currentEnemy.maxHealth)
        {
            playerConfig.Value.currentEnemy.currentHealth = playerConfig.Value.currentEnemy.maxHealth;
        }
        else
        {
            playerConfig.Value.currentEnemy.currentHealth += healthBoost.HealthAmount;
        }
        onEnemyHealthChanged?.Invoke();
        var message = $"{EnemyName} used {healthBoost.Name} to heal for {healthBoost.HealthAmount} health points.";
        Debug.Log(message);

        Action followAction = () =>
        {
            PassToPlayer();
        };

        StartCoroutine(ShowActionText(message, Color.black, 3, followAction));
    }

    public void SetPlayerDefense(Defense defense)
    {
        playerDefenseConfig = new DefenseConfig
        {
            isEnabled = true,
            defenseAmount = defense.DefenseAmount
        };
        var message = $"{PlayerName} used {defense.Name} to shield for {defense.DefenseAmount} hit points.";
        Debug.Log(message);

        TogglePlayerControls(false);

        Action followAction = () =>
        {
            PassToEnemy();
        };

        StartCoroutine(ShowActionText(message, Color.black, 3, followAction));
    }

    public void SetEnemyDefense(Defense defense)
    {
        enemyDefenseConfig = new DefenseConfig
        {
            isEnabled = true,
            defenseAmount = defense.DefenseAmount
        };
        var message = $"{EnemyName} used {defense.Name} to shield for {defense.DefenseAmount} hit points.";
        Debug.Log(message);

        Action followAction = () =>
        {
            PassToPlayer();
        };

        StartCoroutine(ShowActionText(message, Color.black, 3, followAction));
    }

    IEnumerator PlayerWaitBeforeAction()
    {
        yield return new WaitForSeconds(3);
    }

    IEnumerator OnEnemyTurn()
    {
        Debug.Log("ENEMY'S TURN TO ATTACK...");
        yield return new WaitForSeconds(3);
        var actionChooser = UnityEngine.Random.Range(0, 31);
        if (actionChooser <= 10)
        {
            Debug.Log($"THE ACTION CHOOSER CHOSE TO DAMAGE BECAUSE THE VALUE WAS {actionChooser}");
            DamagePlayer(playerConfig.Value.currentEnemy.abilities.Attacks[0]);
        }
        else if (actionChooser <= 20)
        {
            Debug.Log($"THE ACTION CHOOSER CHOSE TO DEFEND BECAUSE THE VALUE WAS {actionChooser}");
            SetEnemyDefense(playerConfig.Value.currentEnemy.abilities.Defenses[0]);
        }
        else
        {
            if (playerConfig.Value.currentEnemy.currentHealth == playerConfig.Value.currentEnemy.maxHealth)
            {
                var actionChooserTieBreaker = UnityEngine.Random.Range(0, 21);
                if (actionChooserTieBreaker <= 10)
                {
                    Debug.Log($"THE TIE BREAKER ACTION CHOOSER CHOSE TO DAMAGE BECAUSE THE VALUE WAS {actionChooserTieBreaker}");
                    DamagePlayer(playerConfig.Value.currentEnemy.abilities.Attacks[0]);
                }
                else
                {
                    Debug.Log($"THE TIE BREAKER ACTION CHOOSER CHOSE TO DEFEND BECAUSE THE VALUE WAS {actionChooserTieBreaker}");
                    SetEnemyDefense(playerConfig.Value.currentEnemy.abilities.Defenses[0]);
                }
            }
            else
            {
                Debug.Log($"THE ACTION CHOOSER CHOSE TO HEAL BECAUSE THE VALUE WAS {actionChooser}");
                HealEnemy(playerConfig.Value.currentEnemy.inventory.HealthBoosts[0]);
            }
        }
        yield return null;
    }

    IEnumerator OnVictory()
    {
        ShowText("VICTORY", Color.green); // TODO make this green not so shitty
        TogglePlayerControls(false);
        // play some sort of victory sound
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(ChangeScenesManager.Instance.previousSceneIndex);
    }

    IEnumerator OnDefeat()
    {
        ShowText("DEFEAT", Color.red);
        TogglePlayerControls(false);
        // play some sort of defeat sound
        yield return new WaitForSeconds(3);
        playerConfig.Value.currentHealth = 1f; // give them a little health to get to the next fight or find some health
        SceneManager.LoadScene(ChangeScenesManager.Instance.previousSceneIndex);
    }

    IEnumerator ShowActionText(string message, Color color, int waitSeconds, Action followAction)
    {
        ShowText(message, color);
        yield return new WaitForSeconds(waitSeconds);
        actionText.gameObject.SetActive(false);
        followAction.Invoke();
    }

    private void PassToEnemy()
    {
        //TogglePlayerControls(false); // doing this earlier so people can't spam... they're gonna try
        Action followAction = () =>
        {
            StartCoroutine(OnEnemyTurn());
        };

        StartCoroutine(ShowActionText($"It's {EnemyName}'s turn to take action", Color.gray, 2, followAction));
    }

    private void PassToPlayer()
    {
        StartCoroutine(PlayerWaitBeforeAction());

        Action followAction = () =>
        {
            TogglePlayerControls(true);
        };

        StartCoroutine(ShowActionText($"It's {PlayerName}'s turn to take action", Color.gray, 2, followAction));
    }

    private void ShowText(string message, Color color)
    {
        var text = actionText.GetComponent<TMP_Text>();
        text.text = message;
        text.color = color;
        actionText.gameObject.SetActive(true);
    }

    private void TogglePlayerControls(bool isActive)
    {
        playerControls.gameObject.SetActive(true);
        foreach (Transform child in playerControls.transform)
        {
            child.gameObject.SetActive(isActive);
        }
    }
}
