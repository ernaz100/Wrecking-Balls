using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] cratePrefabs;
    public GameObject[] boosterPrefabs;
    public GameObject checkpoint;
    public GameObject environment;
    private bool onStartUp = true;
    private int randomCratePrefab;

    void Start()
    {
        SpawnBoostingPads(11);
        SpawnCrateWave(11);
        SpawnCheckpoints(-8);
        SpawnEnvironment(27.93f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCrateWave(float pos)
    {
        if (onStartUp)
        {
            for (int c = 0; c < 5; c++)
            {

                float newPos = pos + (c * 27.5f);

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        randomCratePrefab = Random.Range(0, 2);
                        Instantiate(cratePrefabs[randomCratePrefab], new Vector3(-4 + j, 0f, newPos + i), cratePrefabs[randomCratePrefab].transform.rotation);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    randomCratePrefab = Random.Range(0, 2);
                    Instantiate(cratePrefabs[randomCratePrefab], new Vector3(-4 + j, 0f, pos + i), cratePrefabs[randomCratePrefab].transform.rotation);
                }
            }
        }
    }

    public void SpawnBoostingPads(float pos)
    {
        if (onStartUp)
        {
            for (int c = 0; c < 5; c++)
            {
                float newPos = pos + (c * 27.5f);
                float randomAmount = Random.Range(1, 5);
                for (int i = 0; i < randomAmount; i++)
                {
                    Instantiate(boosterPrefabs[0], GenerateRandomBoosterPosition(newPos), boosterPrefabs[0].transform.rotation);

                }
            }
        }
        else
        {
            float randomAmount = Random.Range(1, 5);
            for (int i = 0; i < randomAmount; i++)
            {
                Instantiate(boosterPrefabs[0], GenerateRandomBoosterPosition(pos), boosterPrefabs[0].transform.rotation);

            }
        }
    }
    public void SpawnCheckpoints(float pos)
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(checkpoint, new Vector3(0.0335958f, -0.485198f, pos + 27.5f * i), checkpoint.transform.rotation);
        }
    }
    public void SpawnEnvironment(float pos)
    {
        if (onStartUp)
        {
            Instantiate(environment, new Vector3(-1.282727f, 4.714375f, pos), environment.transform.rotation);
            Instantiate(environment, new Vector3(-1.282727f, 4.714375f, pos + 30), environment.transform.rotation);
            Instantiate(environment, new Vector3(-1.282727f, 4.714375f, pos + 60), environment.transform.rotation);
            Instantiate(environment, new Vector3(-1.282727f, 4.714375f, pos + 90), environment.transform.rotation);

            onStartUp = false;
        }
        else
        {
            Instantiate(environment, new Vector3(-1.282727f, 4.714375f, pos), environment.transform.rotation);
        }

    }

    private Vector3 GenerateRandomBoosterPosition(float pos)
    {
        float randomX = Random.Range(-4f, 4f);
        float randomZ = Random.Range(pos-16, pos-5);
        return new Vector3(randomX, -0.485f, randomZ);
    }
}
