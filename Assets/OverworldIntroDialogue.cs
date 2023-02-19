using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldIntroDialogue : MonoBehaviour, ISign
{
    [SerializeField] private BoolSO playedIntroSO;

    public Action dialogCloseAction { get; set; }

    public List<string> text { get; set; } = new List<string>
    {
        "That dog was a real jerk",
        "I guess I'll have to find a new home"
    };

    private void SetDialogCloseAction()
    {
        // dialogCloseAction = () =>
        // {
        //     //
        // };

        // dialogCloseAction += () => StartCoroutine(UIManager.Instance.TransitionToArena());
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!playedIntroSO.Value)
        {
            PlayerMovement.Instance.SetMovementEnabled(false);
            playedIntroSO.Value = true;
            StartCoroutine(Intro());
        }
    }

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(1f);
        PlayerMovement.Instance.SetMovementEnabled(true);
        GetComponent<Sign>().Click(false);
    }
}
