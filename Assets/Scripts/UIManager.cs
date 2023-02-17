using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject mainBackground;
    [SerializeField] GameObject arenaBackground;
    [SerializeField] GameObject tileGrid;
    [SerializeField] GameObject arenaCombatUI;

    static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<UIManager>();
            return _instance;
        }
    }

    public void TransitionToArena()
    {
        Debug.Log("TRANSITIONING TO ARENA");
        //CharacterManager.Instance.SetControlsEnabled(false);
        CharacterManager.Instance.player.Enabled = false;
        //mainBackground.SetActive(false);
        //tileGrid.SetActive(false);
        ClickManager.Instance.SetClicksEnabled(false);
        arenaBackground.SetActive(true);
        arenaCombatUI.SetActive(true);
        AudioManager.Instance.TransitionToArena();
        ArenaCombatManager.Instance.StartBattle();
        // play arena transition theme and animation
        // play arena main theme
    }

    public void TransitionToOverworld()
    {
        Debug.Log("TRANSITIONING TO OVERWORLD");
        AudioManager.Instance.TransitionToOverworld();
        // play arena fade out animation
        //tileGrid.SetActive(true);
        //mainBackground.SetActive(true);
        arenaBackground.SetActive(false);
        arenaCombatUI.SetActive(false);
        ArenaCombatManager.Instance.EndBattle();
        //CharacterManager.Instance.SetControlsEnabled(true);
        CharacterManager.Instance.player.Enabled = true;
        ClickManager.Instance.SetClicksEnabled(true);
    }
}
