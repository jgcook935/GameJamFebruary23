using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class SomeCat : MonoBehaviour, ISign
{

    public List<string> text { get; set; } = new List<string>
    {
        "Well hello my young padawan...",
        "If you're going to live out on the streets you're going to need better moves than that.",
        "I'm retired now, so I guess I can teach you my ways.",
        "Good luck out there.",
    };

    public Action dialogCloseAction { get; set; }

    private void SetDialogCloseAction()
    {
        dialogCloseAction = () =>
        {
        };

        dialogCloseAction += () => StartCoroutine(DoStuff());
    }

    private void SetVictoryDialogCloseAction()
    {
        dialogCloseAction = () => { };
    }

    void Awake()
    {
        SetDialogCloseAction();
    }

    IEnumerator DoStuff()
    {
        CharacterManager.Instance.SetLevel2();
        OverworldHUDManager.Instance.UpdateStats();
        ArenaCombatManager.Instance.onPlayerHealthChanged?.Invoke();
        yield return new WaitForSeconds(1);
    }
}
