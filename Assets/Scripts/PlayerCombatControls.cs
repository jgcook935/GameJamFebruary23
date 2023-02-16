using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombatControls : MonoBehaviour
{
    public void Fight()
    {
        Debug.Log("YOU CHOSE TO FIGHT... PUNCH");
    }

    public void Item()
    {
        Debug.Log("YOU CHOSE TO USE AN ITEM, HERE'S A SNICKERS");
    }

    public void Run()
    {
        Debug.Log("YOU CHOSE TO RUN... BITCH");
        SceneManager.LoadScene(ChangeScenesManager.Instance.previousSceneIndex);
    }
}
