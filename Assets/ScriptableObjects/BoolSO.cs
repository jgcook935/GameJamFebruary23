using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolSO : ScriptableObject
{
    [SerializeField]
    private bool _value;
    private bool tempValue;

    public bool Value
    {
        get { return tempValue; }
        set { tempValue = value; }
    }

    private void OnEnable()
    {
        Value = _value;
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    // You can also use OnAfterDeserialize for the other way around
    public void OnAfterDeserialize()
    {
    }
}
