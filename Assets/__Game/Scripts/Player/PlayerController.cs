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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // writing variables for movement
        float zAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");
        movement = new Vector3(xAxis, 0, zAxis);


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

        if (movement.magnitude >= 0.01f)
        {
            rb.velocity = offset;
        }
        else
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
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
