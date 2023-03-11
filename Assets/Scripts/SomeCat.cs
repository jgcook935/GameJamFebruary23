using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class SomeCat : MonoBehaviour, ISign
{
    [SerializeField] BoolSO catTalkedSO;
    private bool flippedDialogue = false;

    public List<string> text { get; set; } = new List<string>
    {
        "You look like you could use a spirit guide...",
        "If you're going to live out on the streets you're going to need better moves than that.",
        "I'm retired now, so I guess I can teach you my ways.",
        "Good luck out there.",
    };

    public List<string> text2 { get; set; } = new List<string>
    {
        "Nice work! You've trained up, gained more health, and learned how to bite",
        "Now get out there and use those teeth!",
    };

    public Action dialogCloseAction { get; set; }

    private void SetDialogCloseAction()
    {
        dialogCloseAction = () =>
        {
        };

        dialogCloseAction += () => StartCoroutine(DoStuff());
    }

    void Awake()
    {
        SetDialogCloseAction();
    }

    void Update()
    {
        if (catTalkedSO.Value && !flippedDialogue)
        {
            text = text2;
            flippedDialogue = true;
        }
    }

    IEnumerator DoStuff()
    {
        AudioManager.Instance.PlayerLevelUpSound();
        CharacterManager.Instance.SetLevel2();
        OverworldHUDManager.Instance.UpdateStats();
        ArenaCombatManager.Instance.onPlayerHealthChanged?.Invoke();
        catTalkedSO.Value = true;
        yield return new WaitForSeconds(1);
    }
}
