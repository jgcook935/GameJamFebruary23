using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        StartCoroutine(TitleUIManager.Instance.TransitionToPlayGame());
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("We quit.");
    }
}
