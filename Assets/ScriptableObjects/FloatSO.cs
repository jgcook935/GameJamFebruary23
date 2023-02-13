using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatSO : ScriptableObject
{
    [SerializeField]
    private float _value;
    private float tempValue;

    public float Value
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
