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
        SpawnFoodBowls();
    }

    private void SpawnFoodBowls()
    {
        for (var i = 0; i < spawnPoints.Length; i++)
        {
            GameObject.Instantiate(foodBowlPrefab, spawnPoints[i].transform);
        }
    }
}
