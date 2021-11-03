using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private Collider crateCollider;
    void Start()
    {
        crateCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.isRed)
        {
            crateCollider.isTrigger = true;
        }
    }
}
