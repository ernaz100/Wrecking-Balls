using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scoreText;
    public GameObject titleScreen;
    public SpawnManager spawnManager;
    private int score;
    public static bool isRunning = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        score = 0;
        scoreText.text = "Score: " + score;
        isRunning = true;
        titleScreen.SetActive(false);
        spawnManager.SpawnBoostingPads();
        spawnManager.SpawnCrateWave();
        spawnManager.SpawnCheckpoint();
        spawnManager.SpawnEnvironment();
    }
    public void EndGame()
    {
        isRunning = false;
        titleScreen.SetActive(true);
    }

    public void UpdateScore(int addToScore)
    {
        score += addToScore;
        scoreText.text = "Score: " + score;
    }
}
