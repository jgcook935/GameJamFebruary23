using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 minPosition;
    public Vector2 maxPosition;
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

    private float smoothing = 0.2f;

    void FixedUpdate()
    {
        if (target == null) target = PlayerMovement.Instance.transform;
        if (transform.position != target.position)
        {
            var targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
