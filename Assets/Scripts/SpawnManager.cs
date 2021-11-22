using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float crateWave_position = 11;
    private float boosterWave_position = 11;
    private float checkpoint_Position = -8;
    private float environment_Position = 27.93f;

    public static int ENVIRONMENT_INTERVAL = 30;
    public static float CRATE_BOOST_CHECKPOINT_INTERVAL = 27.5f;

    public GameObject[] cratePrefabs;
    public GameObject[] boosterPrefabs;
    public GameObject checkpoint;
    public GameObject environment;

    private bool onStartUp = true;

    private int randomCratePrefab;

    void Start()
    {
        SpawnBoostingPads();
        SpawnCrateWave();
        SpawnCheckpoint();
        SpawnEnvironment();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCrateWave()
    {
        if (onStartUp)
        {
            for (int c = 0; c < 5; c++)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        randomCratePrefab = Random.Range(0, 2);
                        Instantiate(cratePrefabs[randomCratePrefab], new Vector3(-4 + j, 0f, crateWave_position + i), cratePrefabs[randomCratePrefab].transform.rotation);
                    }
                }
                crateWave_position += CRATE_BOOST_CHECKPOINT_INTERVAL;
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    randomCratePrefab = Random.Range(0, 2);
                    Instantiate(cratePrefabs[randomCratePrefab], new Vector3(-4 + j, 0f, crateWave_position + i), cratePrefabs[randomCratePrefab].transform.rotation);
                }
            }
            crateWave_position += CRATE_BOOST_CHECKPOINT_INTERVAL;
        }
    }

    public void SpawnBoostingPads()
    {
        if (onStartUp)
        {
            for (int c = 0; c < 5; c++)
            {
                float randomAmount = Random.Range(1, 5);
                for (int i = 0; i < randomAmount; i++)
                {
                    Instantiate(boosterPrefabs[0], GenerateRandomBoosterPosition(boosterWave_position), boosterPrefabs[0].transform.rotation);
                }
                boosterWave_position += CRATE_BOOST_CHECKPOINT_INTERVAL;
            }
        }
        else
        {
            float randomAmount = Random.Range(1, 5);
            for (int i = 0; i < randomAmount; i++)
            {
                Instantiate(boosterPrefabs[0], GenerateRandomBoosterPosition(boosterWave_position), boosterPrefabs[0].transform.rotation);
            }
            boosterWave_position += CRATE_BOOST_CHECKPOINT_INTERVAL;

        }
    }
    public void SpawnCheckpoint()
    {
            Instantiate(checkpoint, new Vector3(0.0335958f, -0.485198f, checkpoint_Position), checkpoint.transform.rotation);
            checkpoint_Position += CRATE_BOOST_CHECKPOINT_INTERVAL;
    }
    public void SpawnEnvironment()
    {
        if (onStartUp)
        {
            Instantiate(environment, new Vector3(-1.282727f, 4.714375f, environment_Position), environment.transform.rotation);
            environment_Position += ENVIRONMENT_INTERVAL;
            Instantiate(environment, new Vector3(-1.282727f, 4.714375f, environment_Position), environment.transform.rotation);
            environment_Position += ENVIRONMENT_INTERVAL;
            Instantiate(environment, new Vector3(-1.282727f, 4.714375f, environment_Position), environment.transform.rotation);
            environment_Position += ENVIRONMENT_INTERVAL;
            Instantiate(environment, new Vector3(-1.282727f, 4.714375f, environment_Position), environment.transform.rotation);
            environment_Position += ENVIRONMENT_INTERVAL;
            onStartUp = false;
        }
        else
        {
            Instantiate(environment, new Vector3(-1.282727f, 4.714375f, environment_Position), environment.transform.rotation);
            environment_Position += ENVIRONMENT_INTERVAL;
        }

    }

    private Vector3 GenerateRandomBoosterPosition(float pos)
    {
        float randomX = Random.Range(-4f, 4f);
        float randomZ = Random.Range(pos-16, pos-5);
        return new Vector3(randomX, -0.485f, randomZ);
    }
}
