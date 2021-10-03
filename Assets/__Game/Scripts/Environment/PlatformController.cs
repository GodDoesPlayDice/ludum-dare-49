using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
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

    public void ToggleRotation(bool onOff)
    {
        if (onOff)
        {
            resetingRotation = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
        } else
        {
            resetingRotation = true;
        }

    }

    private void ResetRotation()
    {
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, Vector3.up), 0.2f));

        if (Vector3.Angle(transform.up, Vector3.up) <= 3)
        {
            rb.MoveRotation(Quaternion.LookRotation(Vector3.forward, Vector3.up));
            resetingRotation = false;
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
}
