using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombatControls : MonoBehaviour
{
    public void Defend()
    {
        // we should show UI here to display what defenses the player has
        Debug.Log("YOU CHOSE TO DEFEND... SAFE CHOICE KID");
    }

    public void Attack()
    {
        // we should show some UI here to display what attacks the player has
        Debug.Log("YOU CHOSE TO ATTACK... PUNCH");
    }

    public void Item()
    {
        // we should show some UI here to display what items the player has
        Debug.Log("YOU CHOSE TO USE AN ITEM, HERE'S A SNICKERS");
    }

    public void Run()
    {
        Debug.Log("YOU CHOSE TO RUN... BITCH");
        StartCoroutine(UIManager.Instance.TransitionToOverworld());
    }
}
