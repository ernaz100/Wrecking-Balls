using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float crateLine_position = 224;
    private float boost_crate_way_position = 11;
    public float checkpoint_Position = -7;
    private float environment_Position = 290f;

    public const float ENVIRONMENT_INTERVAL = 240f;
    public const float BOOST_CHECKPOINT_CRATE_INTERVAL = 240f;
    private const float CRATE_LEFT_BORDER = -6.5f;
    public GameObject[] cratePrefabs;
    public GameObject[] boosterPrefabs;
    public GameObject checkpoint;
    public GameObject environment;
    private int randomPrefab;

    public void SpawnCrateLine()
    {
       
        for (int j = 0; j < 18; j += 2)
        {
            randomPrefab = Random.Range(0, 3);
            Instantiate(cratePrefabs[randomPrefab], new Vector3(CRATE_LEFT_BORDER + j, 0f, crateLine_position), cratePrefabs[randomPrefab].transform.rotation);
        }
        crateLine_position += BOOST_CHECKPOINT_CRATE_INTERVAL;
       
    }
   
    public void SpawnCheckpoint()
    {
            Instantiate(checkpoint, new Vector3(1.25f, -0.485198f, checkpoint_Position), checkpoint.transform.rotation);
            checkpoint_Position += BOOST_CHECKPOINT_CRATE_INTERVAL;
    }
    public void SpawnEnvironment()
    {
       
        Instantiate(environment, new Vector3(-1.282727f, 4.714375f, environment_Position), environment.transform.rotation);
        environment_Position += ENVIRONMENT_INTERVAL;
       
    }
    public void SpawnBoostingPadsAndRandomCrates()
    {
        float randomAmount = Random.Range(10, 15);
        for (int j = 0; j < randomAmount; j++)
        {
            randomPrefab = Random.Range(0, 3);
            Instantiate(cratePrefabs[randomPrefab], GenerateRandomCratePosition(boost_crate_way_position), cratePrefabs[randomPrefab].transform.rotation);
        }
       /* randomAmount = Random.Range(1, 5);
        for (int i = 0; i < randomAmount; i++)
        {
            randomPrefab = Random.Range(0, 2);
            Instantiate(boosterPrefabs[randomPrefab], GenerateRandomBoosterPosition(boost_crate_way_position), boosterPrefabs[randomPrefab].transform.rotation);
        } */
        boost_crate_way_position += BOOST_CHECKPOINT_CRATE_INTERVAL;
    }

    private Vector3 GenerateRandomBoosterPosition(float pos)
    {
        float randomX = Random.Range(-7f, 7);
        float randomZ = Random.Range(pos, pos+150);
        return new Vector3(randomX, -0.485f, randomZ);
    }
    private Vector3 GenerateRandomCratePosition(float pos)
    {
        float randomX = Random.Range(-7f,7f);
        float randomZ = Random.Range(pos, pos + 150);
        return new Vector3(randomX, 0, randomZ);
    }
}
