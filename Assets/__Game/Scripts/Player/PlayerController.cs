using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // movement variables
    public float movementSpeed = 4f;
    private Rigidbody rb;
    private Vector3 movement;

    // animation variables
    private Animator animator;

    // platform
    private Transform platformTransform;


    // test
    private bool platformRotation = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        platformTransform = GameObject.FindGameObjectWithTag("Platform").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // input for movement
        float zAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");
        movement = new Vector3(xAxis, 0, zAxis);

        // input for action
        if (Input.GetKeyDown(KeyCode.Space))
        {
            platformRotation = !platformRotation;
            GameObject.FindGameObjectWithTag("Platform").GetComponent<PlatformController>().ToggleRotation(platformRotation);
        }


        // animation
        if (animator != null)
        {
            if (movement.magnitude >= 0.1f)
            {
                animator.SetFloat("speed", movement.magnitude);
            } else
            {
                animator.SetFloat("speed", 0);
            }
        }
        
    }

    private void FixedUpdate()
    {
        Movement();
        Rotation();
    }

    private void Movement()
    {
        if (rb == null) return;
        Vector3 offset = movement * movementSpeed;

        // no movement if platform is too inclined
        if (platformTransform != null)
        {
            if (Vector3.Angle(platformTransform.up, Vector3.up) > 30)
            {
                offset = rb.velocity;
            }
        }

        // movement
        if (movement.magnitude >= 0.01f)
        {
            rb.velocity = new Vector3(offset.x, rb.velocity.y, offset.z);
        }
    }

    private void Rotation()
    {
        if  (movement.magnitude >= 0.1f)
        {
            rb.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement, Vector3.up), 0.25f));
        }
    }
}
