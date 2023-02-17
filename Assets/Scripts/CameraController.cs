using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 minPosition;
    public Vector2 maxPosition;
    [SerializeField] private Vector2SO previousSceneCameraMin;
    [SerializeField] private Vector2SO previousSceneCameraMax;

    private Transform target;

    static CameraController _instance;
    public static CameraController Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<CameraController>();
            return _instance;
        }
    }

    private float smoothing = 0.15f;
    private bool snappedToTarget = false;

    private Vector3 targetPosition
    {
        get
        {
            return new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }

    private Vector3 targetPositionClamped
    {
        get
        {
            var tpc = targetPosition;
            tpc.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            tpc.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            return tpc;
        }
    }

    void Awake()
    {
        if (ChangeScenesManager.Instance.previousSceneIndex == 3)
        {
            minPosition = previousSceneCameraMin.Value;
            maxPosition = previousSceneCameraMax.Value;
        }
    }

    void FixedUpdate()
    {
        if (target == null) target = PlayerMovement.Instance.transform;

        if (!snappedToTarget)
        {
            transform.position = ChangeScenesManager.Instance.previousSceneIndex == 3 ? targetPositionClamped : targetPosition;
            snappedToTarget = true;
            return;
        }

        if (transform.position != target.position)
        {
            transform.position = Vector3.Lerp(transform.position, targetPositionClamped, smoothing);
        }
    }
}
