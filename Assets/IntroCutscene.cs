using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutscene : MonoBehaviour
{
    [SerializeField] RacoonMoment moment;

    void Start()
    {
        CharacterManager.Instance.player.SetMovementEnabled(false);
        moment.enabled = false;
        StartCoroutine(IntroStuff());
    }

    IEnumerator IntroStuff()
    {
        yield return new WaitForSeconds(2);
        moment.enabled = true;
        yield return new WaitForSeconds(1);
        moment.TriggerMoment();
    }
}
