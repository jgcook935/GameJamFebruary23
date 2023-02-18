using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodBowlSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] GameObject foodBowlPrefab;

    static FoodBowlSpawner _instance;
    public static FoodBowlSpawner Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<FoodBowlSpawner>();
            return _instance;
        }
    }

    private void Awake()
    {
        SpawnNewFoodBowl(3);
    }

    private void SpawnNewFoodBowl(int previousSpawn)
    {
        var nextSpawn = previousSpawn + 1;
        if (Enumerable.Range(0, spawnPoints.Length).Contains(nextSpawn))
        {
            Debug.Log($"NEXT SPAWN IS {nextSpawn}. AND FALLS WITHIN A HEALTHY RANGE. USING IT.");
        }
        else
        {
            nextSpawn = previousSpawn - 1;
            if (Enumerable.Range(0, spawnPoints.Length).Contains(nextSpawn))
            {
                Debug.Log($"NEXT SPAWN IS {nextSpawn}. AND FALLS WITHIN A HEALTHY RANGE. USING IT.");
            }
            else
            {
                nextSpawn = 0;
                Debug.Log($"COULDN'T GET A NEW SPAWN POINT ORGANICALLY, GOING TO USE 0");
            }
        }

        var foodBowl = GameObject.Instantiate(foodBowlPrefab, spawnPoints[nextSpawn].transform);
        foodBowl.GetComponent<FoodBowl>().SetOnFoodEatenAction(() => SpawnNewFoodBowl(nextSpawn));
    }
}
