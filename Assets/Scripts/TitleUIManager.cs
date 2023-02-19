using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] GameObject crossfade;
    private Animator crossfadeAnimator;

    static TitleUIManager _instance;
    public static TitleUIManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<TitleUIManager>();
            return _instance;
        }
    }

    private void Awake()
    {
        crossfade.gameObject.SetActive(true);
        crossfadeAnimator = crossfade.GetComponent<Animator>();
    }

    public IEnumerator TransitionToPlayGame()
    {
        TitleAudioManager.Instance.audioSource.Stop();
        crossfadeAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(3);
    }
}