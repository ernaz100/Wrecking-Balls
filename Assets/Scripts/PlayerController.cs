using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 20f;
    public float sideSpeed = 20f;
    public float boostingSpeed = 50f;

    public Material strongerMaterial;
    public ParticleSystem blueExplosion;
    public ParticleSystem redExplosion;

    private bool onCheckpoint = true;
    private bool isRed = false;
    private bool gameOver = false;
    private bool onStart = true;
    private Coroutine gameOverCountDown;
    private Rigidbody playerRb;
    private GameManager gameManager;
    private SpawnManager spawnManager;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            gameManager.EndGame();
        }
        else
        {

            MoveSideways();

            if (Input.GetKeyDown(KeyCode.Space) && onCheckpoint)
            {
                playerRb.AddForce(Vector3.forward * forwardSpeed , ForceMode.Impulse); 
                onCheckpoint = false;
                gameOverCountDown = StartCoroutine(DetectGameOver());

            }
         }
    }

    private void MoveSideways()
    {
        if (GameManager.isRunning)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            playerRb.AddForce(Vector3.right * Time.deltaTime * sideSpeed * horizontalInput, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boost"))
        {
            isRed = true;
            playerRb.AddForce(Vector3.forward * boostingSpeed, ForceMode.Impulse);
            GetComponent<Renderer>().material = strongerMaterial;
        }
        else if (other.CompareTag("Checkpoint"))
        {
            onCheckpoint = true;
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            if (!onStart)
            {
                StopCoroutine(gameOverCountDown);
            }

        }

    }   
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            spawnManager.SpawnCrateWave();
            spawnManager.SpawnBoostingPads();
            spawnManager.SpawnEnvironment();
            spawnManager.SpawnCheckpoint();
            onStart = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isRed && collision.gameObject.CompareTag("Red Crate"))
        {
            playerRb.AddForce(Vector3.forward , ForceMode.Impulse);
            Destroy(collision.gameObject.gameObject);
            Instantiate(redExplosion, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            gameManager.UpdateScore(20);


        }
        if (collision.gameObject.CompareTag("Blue Crate"))
        {
            playerRb.AddForce(Vector3.forward , ForceMode.Impulse);
            Destroy(collision.gameObject.gameObject);
            Instantiate(blueExplosion, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            gameManager.UpdateScore(10);

        }
    }

    IEnumerator DetectGameOver()
    {
        yield return new WaitForSeconds(10);
        gameOver = true;
    }


}
