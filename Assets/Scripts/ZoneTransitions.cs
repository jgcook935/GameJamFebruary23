using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTransitions : MonoBehaviour
{
    public Vector2 newMinPos;
    public Vector2 newMaxPos;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraController.Instance.maxPosition = newMaxPos;
            CameraController.Instance.minPosition = newMinPos;
        }
    }
}
