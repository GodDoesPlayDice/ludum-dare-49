using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float yRotationSpeed = 5;

    private Rigidbody rb;
    private bool resetingRotation = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (resetingRotation)
        {
            ResetRotation();
        }
    }

    public void ToggleZXRotation(bool onOff)
    {
        if (onOff)
        {
            resetingRotation = false;
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationY;
        } else
        {
            resetingRotation = true;
        }
    }

    public void ToggleYRotation(bool onOff)
    {
        if (onOff)
        {
            resetingRotation = false;
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            rb.angularVelocity = new Vector3(rb.angularVelocity.x, yRotationSpeed, rb.angularVelocity.z);
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            rb.angularVelocity = Vector3.zero;
            //resetingRotation = true;
        }

    }

    private void ResetRotation()
    {
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, Vector3.up), 0.18f));

        if (Vector3.Angle(transform.up, Vector3.up) <= 3)
        {
            rb.MoveRotation(Quaternion.LookRotation(Vector3.forward, Vector3.up));
            resetingRotation = false;
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
}
