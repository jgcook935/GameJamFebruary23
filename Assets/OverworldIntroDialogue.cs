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
        "Ugh that dog is a real jerk",
        "There's no place for me there anymore, my time with that family has come to an end.",
        "I guess I'm on my own..."
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
