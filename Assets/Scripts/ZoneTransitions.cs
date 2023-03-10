using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTransitions : MonoBehaviour
{
    public Vector2 newMinPos;
    public Vector2 newMaxPos;
    public Transform teleportLocation;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraController.Instance.maxPosition = newMaxPos;
            CameraController.Instance.minPosition = newMinPos;
            if (teleportLocation != null)
            {
                CharacterManager.Instance.player.transform.position = new Vector3(teleportLocation.position.x, teleportLocation.position.y, CharacterManager.Instance.player.transform.position.z);
                CameraController.Instance.transform.position = teleportLocation.position;
            }
        }
    }
}
