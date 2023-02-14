using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.FilePathAttribute;

public class ChangeScenesManager : MonoBehaviour
{
    [SerializeField] TransitionTrigger[] transitions;
    [SerializeField] Vector2SO sceneLocationSO_1;
    [SerializeField] Vector2SO sceneLocationSO_2;
    [SerializeField] Vector2SO sceneLocationSO_3;

    static ChangeScenesManager _instance;
    public static ChangeScenesManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<ChangeScenesManager>();
            return _instance;
        }
    }

    public void SetSceneLocation(Vector2 location)
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                {
                    Debug.Log("This should be the title sceen");
                    break;
                }
            case 1:
                {
                    sceneLocationSO_1.Value = location;
                    break;
                }
            case 2:
                {
                    sceneLocationSO_2.Value = location;
                    break;
                }
            case 3:
                {
                    sceneLocationSO_3.Value = location;
                    break;
                }
            default:
                {
                    Debug.Log("Invalid scene index");
                    break;
                }
        }
    }

    public Vector2 GetSceneLocation()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                {
                    Debug.Log("This should be the title sceen");
                    return Vector2.zero;
                }
            case 1:
                {
                    return sceneLocationSO_1.Value;
                }
            case 2:
                {
                    return sceneLocationSO_2.Value;
                }
            case 3:
                {
                    return sceneLocationSO_3.Value;
                }
            default:
                {
                    Debug.Log("ChangeScenesManager current scene not setup for scene transitions");
                    return Vector2.zero;
                }
        }
    }
}
