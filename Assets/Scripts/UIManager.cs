using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject mainBackground;
    [SerializeField] GameObject arenaBackground;
    [SerializeField] GameObject tileGrid;
    [SerializeField] GameObject arenaCombatUI;
    [SerializeField] GameObject crossfade;

    private Animator crossfadeAnimator;

    private void Awake()
    {
        crossfade.gameObject.SetActive(true);
        crossfadeAnimator = crossfade.GetComponent<Animator>();
    }

    static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<UIManager>();
            return _instance;
        }
    }

    public IEnumerator TransitionToArena()
    {
        AudioManager.Instance.LeaveOverWorld();
        crossfadeAnimator.SetTrigger("Start");

        AudioManager.Instance.PlayTransitionTheme();

        yield return new WaitForSeconds(2.857f);

        crossfadeAnimator.SetTrigger("End");

        CharacterManager.Instance.player.Enabled = false;
        ClickManager.Instance.SetClicksEnabled(false);
        arenaBackground.SetActive(true);
        arenaCombatUI.GetComponent<CanvasGroup>().alpha = 1;
        arenaCombatUI.GetComponent<CanvasGroup>().interactable = true;
        arenaCombatUI.GetComponent<CanvasGroup>().blocksRaycasts = true;

        AudioManager.Instance.TransitionToArena();
        ArenaCombatManager.Instance.StartBattle();

        yield return null;
    }

    public IEnumerator TransitionToOverworld()
    {
        arenaCombatUI.GetComponent<CanvasGroup>().alpha = 0;
        arenaCombatUI.GetComponent<CanvasGroup>().interactable = false;
        arenaCombatUI.GetComponent<CanvasGroup>().blocksRaycasts = false;
        AudioManager.Instance.LeaveArena();

        crossfadeAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(3f);

        crossfadeAnimator.SetTrigger("End");

        arenaBackground.SetActive(false);
        ArenaCombatManager.Instance.EndBattle();

        //yield return new WaitForSeconds(2);

        CharacterManager.Instance.player.Enabled = true;
        ClickManager.Instance.SetClicksEnabled(true);
        AudioManager.Instance.TransitionToOverworld();

        yield return null;
    }
}
