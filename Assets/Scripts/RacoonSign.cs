using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacoonSign : MonoBehaviour, ISign
{
    [SerializeField] Sprite localSprite;
    [SerializeField] AudioClip[] localHurtSounds;

    public List<string> text { get; set; } = new List<string>
    {
        "Well, well, well...",
        "Look at the fancy housecat",
        "Run on home to your family, housecat",
        "Oh you want to show me what you got?",
    };

    public Action dialogCloseAction { get; set; }

    private void SetDialogCloseAction()
    {
        dialogCloseAction = () =>
        {
            //
        };

        dialogCloseAction += () => StartCoroutine(UIManager.Instance.TransitionToArena());
    }

    void Awake()
    {
        SetDialogCloseAction();
    }
}
