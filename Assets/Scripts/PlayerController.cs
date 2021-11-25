using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const int SHRINKING_DELAY = 6;
    private const int BLUE = 0, RED = 1, YELLOW = 2;
    public float forwardSpeed = 50f;
    public float sideSpeed = 20f;
    public float boostingSpeed = 50f;
    public Vector3 velocity;


    public Material[] playerMaterial;
    public ParticleSystem[] explosions;

    private bool onCheckpoint = true;
    [HideInInspector]
    public bool isRed = false;
    [HideInInspector]
    public bool isYellow = false;

    IEnumerator changeColor;
    public Rigidbody playerRb;
    private GameManager gameManager;
    private SpawnManager spawnManager;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        changeColor = ChangeColor();
    }

    // Update is called once per frame
    void Update()
    {
            MoveSideways();
        Debug.Log(spawnManager.checkpoint_Position);
        velocity = playerRb.GetPointVelocity(transform.position);
            if(transform.position.z >= spawnManager.checkpoint_Position-240 - 2.5f && transform.position.z <= spawnManager.checkpoint_Position-240 + 2.5f)
            {
                 onCheckpoint = true;
            }
            else
            {
                 onCheckpoint = false;
            }
        
            if (onCheckpoint && GameManager.isRunning)
            {
                transform.Translate(new Vector3(0,0,1) * Time.deltaTime,Space.World);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StopCoroutine(changeColor);
                    playerRb.AddForce(Vector3.forward * 50, ForceMode.Impulse);
                }
            }
            else if(!onCheckpoint && GameManager.isRunning)
            {
                playerRb.AddForce(Vector3.forward * forwardSpeed * Time.deltaTime, ForceMode.Impulse);
                transform.localScale -= new Vector3(1,1,1) * Time.deltaTime / SHRINKING_DELAY;
                transform.position = new Vector3(transform.position.x,((transform.localScale.x / 2)) , transform.position.z);
                if(transform.localScale.x < 0 )
                {
                    playerRb.velocity = Vector3.zero;
                    playerRb.angularVelocity = Vector3.zero;
                    gameManager.EndGame();
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
        if (other.CompareTag("Checkpoint"))
        {
            onCheckpoint = true;
            transform.position = new Vector3(transform.position.x, transform.position.y, spawnManager.checkpoint_Position - 242.5f);
            StartCoroutine(changeColor);
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            transform.localScale = new Vector3(1, 1, 1);
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;

        }
    }   
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            StopCoroutine(changeColor);
            spawnManager.SpawnCrateLine();
            spawnManager.SpawnBoostingPadsAndRandomCrates();
            spawnManager.SpawnEnvironment();
            spawnManager.SpawnCheckpoint();
            onCheckpoint = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isRed && !isYellow && collision.gameObject.CompareTag("Blue Crate"))
        {
            Destroy(collision.gameObject.gameObject);
            playerRb.velocity = velocity;
            Instantiate(explosions[BLUE], collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            gameManager.UpdateScore(10);

        }
        if (isRed && collision.gameObject.CompareTag("Red Crate"))
        {
            Destroy(collision.gameObject.gameObject);
            playerRb.velocity = velocity;
            Instantiate(explosions[RED], collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            gameManager.UpdateScore(20);


        }
        if(isYellow && collision.gameObject.CompareTag("Yellow Crate"))
        {
            Destroy(collision.gameObject.gameObject);
            playerRb.velocity = velocity;
            Instantiate(explosions[YELLOW], collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            gameManager.UpdateScore(30);
        }
    }
    IEnumerator ChangeColor()
    {
        while (onCheckpoint)
        {
            GetComponent<Renderer>().material = playerMaterial[BLUE];
            isRed = false;
            isYellow = false;
            yield return new WaitForSeconds(0.5f);
            GetComponent<Renderer>().material = playerMaterial[RED];
            isRed = true;
            yield return new WaitForSeconds(0.5f);
            GetComponent<Renderer>().material = playerMaterial[YELLOW];
            isRed = false;
            isYellow = true;
            yield return new WaitForSeconds(0.5f);


        }

    }



}
