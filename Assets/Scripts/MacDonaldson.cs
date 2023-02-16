using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class MacDonaldson : MonoBehaviour, ISign
{
    public List<string> text { get; set; } = new List<string>
    {
        "Get outta here kid",
        "...",
        "What do you wanna fight me or something?"
    };

    public Action dialogCloseAction { get; set; } = () =>
    {
        ChangeScenesManager.Instance.LoadScene(CharacterManager.Instance.player.transform.position, 3); // 3 is the arena scene
    };
}
