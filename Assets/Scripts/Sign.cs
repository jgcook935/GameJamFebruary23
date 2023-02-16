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

    public void Click()
    {
        if (dialogBox != null) return;

        dialogBox = Instantiate(dialogBoxPrefab, transform);
        var controller = dialogBox.GetComponent<DialogBoxController>();
        var sign = GetComponent<ISign>();
        controller.SetText(sign.text);
        controller.SetDialogCloseAction(sign.dialogCloseAction);
    }
}
