using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] cratePrefabs;
    public GameObject[] boosterPrefabs;
    void Start()
    {
        SpawnCrateWave();
        SpawnBoostingPads();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnCrateWave()
    {
        for(int i =0; i < 5; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Instantiate(cratePrefabs[j % 2], new Vector3(-4 + j, 0.5f, 11+i), cratePrefabs[j % 2].transform.rotation);
            }
        }
        
    }

    void SpawnBoostingPads()
    {
        
        float randomAmount = Random.Range(1, 5);
        Debug.Log(randomAmount);
        for(int i = 0; i < randomAmount; i++)
        {
            Instantiate(boosterPrefabs[0], GenerateRandomBoosterPosition(), boosterPrefabs[0].transform.rotation);

        }
    }

    private Vector3 GenerateRandomBoosterPosition()
    {
        float randomX = Random.Range(-4f, 4f);
        float randomZ = Random.Range(-5f, 6f);
        return new Vector3(randomX, -0.485f, randomZ);
    }
}
