using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TransitionTrigger : MonoBehaviour
{
    [SerializeField] int sceneToSwitchToIndex;
    public Transform spawnTransform;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") return;
        LoadScene();
    }

    void LoadScene()
    {
        ChangeScenesManager.Instance.LoadScene(spawnTransform.position, sceneToSwitchToIndex);
    }
}
