using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI[] moneyText;
    public GameObject titleScreen;
    public GameObject endScreen;
    public PlayerController playerController;
    public SpawnManager spawnManager;
    private int score;
    private int coins;
    private float seconds;
    public static bool isRunning = false;

    void Start()
    {
        moneyText[0].text = "$: " + Getint("WreckingCoins");
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning && !playerController.onCheckpoint)
        {
            seconds += Time.deltaTime;
            if (seconds > 0.5f)
            {
                UpdateScore(1);
                seconds = 0;
            }
        }
        
    }

    public void StartGame()
    {
        score = 0;
        scoreText.text = "Score: " + score;
        isRunning = true;
        titleScreen.SetActive(false);
        spawnManager.SpawnBoostingPadsAndRandomCrates();
        spawnManager.SpawnCrateLine();
        spawnManager.SpawnCheckpoint();
        spawnManager.SpawnEnvironment();
    }
    public void EndGame()
    {
        isRunning = false;
        coins = Getint("WreckingCoins") + score / 100;
        moneyText[1].text = "$: " + coins;
        SetInt("WreckingCoins", coins);
        endScreen.SetActive(true);
       
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateScore(int addToScore)
    {
        score += addToScore;
        scoreText.text = "Score: " + score;
    }

    public void SetInt(string KeyName, int Value)
    {
        PlayerPrefs.SetInt(KeyName, Value);
    }

    public int Getint(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }
}
