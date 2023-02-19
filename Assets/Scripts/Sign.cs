using UnityEngine;
using System.Collections.Generic;
using System;

public interface ISign
{
    List<string> text { get; set; }
    Action dialogCloseAction { get; set; }
}

public class Sign : MonoBehaviour, IClickable
{
    [SerializeField] GameObject dialogBoxPrefab;
    GameObject dialogBox;
    public List<string> text { get; set; }

    public void Click(bool overrideText)
    {
        if (dialogBox != null)
        {
            Debug.Log("we're returning because dialog box is not null");
            return;
        }

        dialogBox = Instantiate(dialogBoxPrefab, transform);
        var controller = dialogBox.GetComponent<DialogBoxController>();
        var sign = GetComponent<ISign>();
        if (CharacterManager.Instance.playerConfigSO.Value.currentHealth <= 0 && overrideText)
        {
            Debug.Log($"WE'RE override text because player health is {CharacterManager.Instance.playerConfigSO.Value.currentHealth} and overrideText is {overrideText}");
            controller.SetText(new List<string>
            {
                "You look seriously hurt.",
                "Come back when you've had something to eat."
            });
            controller.SetDialogCloseAction(() => Debug.Log("PLAYER DOESN'T HAVE HEALTH, NEED TO HEAL UP BEFORE A FIGHT"));
        }
        else
        {
            Debug.Log($"WE'RE SETTING NORMAL TEXT BECAUSE PLAYER HEALTH IS {CharacterManager.Instance.playerConfigSO.Value.currentHealth} and overrideText is {overrideText}");
            controller.SetText(sign.text);
            controller.SetDialogCloseAction(sign.dialogCloseAction);
        }
    }
}
