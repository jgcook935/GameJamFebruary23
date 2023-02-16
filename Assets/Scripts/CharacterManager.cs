using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CharacterManager : MonoBehaviour
{
    static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<CharacterManager>();
            return _instance;
        }
    }

    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject mainCameraPrefab;

    private CameraController cameraController;
    public PlayerMovement player;

    public void SetControlsEnabled(bool enabled)
    {
        player.SetMovementEnabled(enabled);
    }

    void Awake()
    {
        cameraController = Instantiate(mainCameraPrefab, transform).GetComponent<CameraController>();
        player = Instantiate(playerPrefab, transform).GetComponent<PlayerMovement>();
        cameraController.target = player.transform;

        var sceneLocation = ChangeScenesManager.Instance.GetSceneLocation();
        if (sceneLocation != Vector2.zero) player.transform.position = sceneLocation;
    }
}
