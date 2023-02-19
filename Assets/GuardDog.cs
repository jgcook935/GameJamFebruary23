using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GuardDog : MonoBehaviour
{
    [SerializeField] private GameObject dogPrefab;

    private GameObject dog;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && dog == null)
        {
            Debug.Log("Spawning Dog");
            dog = Instantiate(dogPrefab, transform);
            dog.GetComponent<AIDestinationSetter>().target = PlayerMovement.Instance.transform;
            // play bark sound
            StartCoroutine(WaitBeforeAction());
        }
    }

    IEnumerator WaitBeforeAction()
    {
        yield return new WaitForSeconds(1.5f);
        dog.GetComponent<AIDestinationSetter>().target = transform;
        yield return new WaitForSeconds(3);
        Destroy(dog);
    }
}
