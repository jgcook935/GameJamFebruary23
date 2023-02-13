using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntSO : ScriptableObject
{
    [SerializeField]
    private int _value;
    private int tempValue;

    public int Value
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
