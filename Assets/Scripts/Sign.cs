using UnityEngine;
using System.Collections.Generic;

public interface ISign
{
    List<string> text { get; set; }
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
        dialogBox.GetComponent<DialogBoxController>().SetText(GetComponent<ISign>().text);
    }
}
