using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutscene : MonoBehaviour
{
    [SerializeField] RacoonMoment moment;

    void Awake()
    {
        moment.enabled = false;
        CharacterManager.Instance.player.Enabled = false;
        StartCoroutine(IntroStuff());
    }

    IEnumerator IntroStuff()
    {
        yield return new WaitForSeconds(3);
        moment.enabled = true;
        moment.TriggerMoment();
    }
}
