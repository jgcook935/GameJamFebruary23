using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Vector2SO : ScriptableObject
{
    [SerializeField]
    private Vector2 _value;
    private Vector2 tempValue;

    public Vector2 Value
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
