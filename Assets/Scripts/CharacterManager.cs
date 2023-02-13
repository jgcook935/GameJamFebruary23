using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private PlayerMovement player;

    void Start()
    {
        cameraController = Instantiate(mainCameraPrefab, transform).GetComponent<CameraController>();
        player = Instantiate(playerPrefab, transform).GetComponent<PlayerMovement>();

        var sceneLocation = ChangeScenesManager.Instance.GetSceneLocation();
        if (sceneLocation != Vector2.zero) player.transform.position = sceneLocation;
    }

    void Update()
    {
        if (!player.enabled)
        {
            player.animator.SetFloat("Horizontal", player.movement.x);
            player.animator.SetFloat("Vertical", player.movement.y);
            player.animator.SetFloat("Speed", player.movement.sqrMagnitude);
            player.animator.SetBool("IsPlayerTwo", false);
        }
    }
}
