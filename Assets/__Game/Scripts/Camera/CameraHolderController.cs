using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolderController : MonoBehaviour
{
    public Vector3 positionOffset;
    public float smoothForMovement = 0.01f;

    private Transform playerTransform;
    private Vector3 target;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        if (playerTransform != null)
        {
            if (playerTransform.position.y > -3)
            {
                target = Vector3.Lerp(transform.position, playerTransform.position + positionOffset, smoothForMovement);
                target.x = target.x/1.3f;
                transform.position = target;
            } else
            {
                target = Vector3.Lerp(transform.position, positionOffset, 0.01f);
                transform.position = target;
            }
            
        }
    }
}
