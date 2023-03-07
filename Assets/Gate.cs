using System.Collections;
using UnityEngine;

public class Gate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private BoolSO beatPersonSO;
    private bool destroyedChildren = false;

    // Update is called once per frame
    void Update()
    {
        if (!destroyedChildren && beatPersonSO.Value == true)
        {
            foreach (Transform child in transform)
            {
                destroyedChildren = true;
                StartCoroutine(DestroyGates(child));
            }
        }
    }

    IEnumerator DestroyGates(Transform child)
    {
        yield return new WaitForSeconds(7f);
        AudioManager.Instance.PlayGateSound();
        Destroy(child.gameObject);
    }

}
