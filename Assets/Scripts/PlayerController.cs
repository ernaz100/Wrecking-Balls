using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float forwardSpeed = 10f;
    public float sideSpeed = 10f;
    private float zBound = 30;
    private bool isStarted = false;
    private Rigidbody playerRb;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveSideways();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isStarted = true;
        }
        if (isStarted)
        {
            MoveForward();
        }
        ConstraintPlayerMovement();
    }
    private void MoveForward()
    {
        playerRb.AddForce(Vector3.forward * Time.deltaTime * forwardSpeed, ForceMode.Impulse);
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
    
}
