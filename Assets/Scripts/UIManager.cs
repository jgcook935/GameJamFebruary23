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
        crossfadeAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(2);

        crossfadeAnimator.SetTrigger("End");
        crossfadeAnimator.SetTrigger("Idle");

        CharacterManager.Instance.player.Enabled = false;
        ClickManager.Instance.SetClicksEnabled(false);
        arenaBackground.SetActive(true);
        arenaCombatUI.SetActive(true);

        AudioManager.Instance.TransitionToArena();
        ArenaCombatManager.Instance.StartBattle();

        yield return null;
    }

    public IEnumerator TransitionToOverworld()
    {
        crossfadeAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(2);

        crossfadeAnimator.SetTrigger("End");
        crossfadeAnimator.SetTrigger("Idle");

        arenaCombatUI.SetActive(false);
        AudioManager.Instance.TransitionToOverworld();
        arenaBackground.SetActive(false);
        ArenaCombatManager.Instance.EndBattle();
        CharacterManager.Instance.player.Enabled = true;
        ClickManager.Instance.SetClicksEnabled(true);

        yield return null;
    }
}
