using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfVision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnBecameInvisible()
    {
        if (gameObject.CompareTag("Env"))
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
