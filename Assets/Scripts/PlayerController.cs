using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float forwardSpeed = 20f;
    public float sideSpeed = 20f;
    public float boostingSpeed = 50f;
    private float zBound = 30;
    private bool isStarted = false;

    public static bool isRed = false;

    private Rigidbody playerRb;
    public Material strongerMaterial;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveSideways();

        if (Input.GetKeyDown(KeyCode.Space) && isStarted == false)
        {
            playerRb.AddForce(Vector3.forward * Time.deltaTime * forwardSpeed * 300, ForceMode.Impulse); 
            isStarted = true;
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
        else if (other.CompareTag("Blue Crate"))
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Red Crate"))
        {
            Destroy(other.gameObject);
        }

    }


}
