using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float forwardSpeed = 20f;
    public float sideSpeed = 20f;
    public float boostingSpeed = 50f;
    public static bool isRed = false;

    public Material strongerMaterial;
    public ParticleSystem blueExplosion;
    public ParticleSystem redExplosion;

    private float zBound = -8;
    private float environmentPos = 117.93f;
    private bool onCheckpoint = true;
    private float checkpointPos = -8f;
    private float cratePos = 148.5f;
    private int spawnCountdown = 3;
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
        MoveSideways();

        if (Input.GetKeyDown(KeyCode.Space) && onCheckpoint)
        {
            playerRb.AddForce(Vector3.forward * forwardSpeed , ForceMode.Impulse); 
            onCheckpoint = false;
            zBound += 27.5f;
            spawnCountdown--;
        }

        ConstraintPlayerMovement();
    }
    
    private void MoveSideways()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(Vector3.right * Time.deltaTime * sideSpeed * horizontalInput, ForceMode.Impulse);
    }
    private void ConstraintPlayerMovement()
    {
        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
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



        }

    }   
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Checkpoint") && spawnCountdown == 0)
        {
            spawnManager.SpawnCrateWave(cratePos);
            spawnManager.SpawnBoostingPads(cratePos);
            cratePos += 27.5f;
            checkpointPos += 3 * 27.5f;
            environmentPos += 30;
            spawnManager.SpawnEnvironment(environmentPos);
            spawnManager.SpawnCheckpoints(checkpointPos);
            spawnCountdown = 3;
        }
        else if (other.CompareTag("Checkpoint"))
        {
            environmentPos += 30;
            spawnManager.SpawnEnvironment(environmentPos);
            spawnManager.SpawnCrateWave(cratePos);
            spawnManager.SpawnBoostingPads(cratePos);
            cratePos += 27.5f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isRed && collision.gameObject.CompareTag("Red Crate"))
        {
            Destroy(collision.gameObject.gameObject);
            Instantiate(redExplosion, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            gameManager.UpdateScore(20);


        }
        if (collision.gameObject.CompareTag("Blue Crate"))
        {
            Destroy(collision.gameObject.gameObject);
            Instantiate(blueExplosion, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            gameManager.UpdateScore(10);

        }
    }


}
