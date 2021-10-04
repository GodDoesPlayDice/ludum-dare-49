using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // y rotation
    public float yRotationSpeed = 5;

    // scale
    public float maxScaleMultiplier = 3;
    public float scaleSmooth = 0.1f;

    // zx rotation

    private Rigidbody rb;
    private bool resetingRotation = true;
    private bool resetingScale = true;
    private bool growingScale = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnHappening(HappeningEP happeningEP)
    {
        if (happeningEP.type != HappeningType.CRAZY) return;
        if (happeningEP.happening.crazyType == CrazyHappeningType.scale) ToggleScale(happeningEP.isOn);
        if (happeningEP.happening.crazyType == CrazyHappeningType.xzRotation) ToggleXZRotation(happeningEP.isOn);
        if (happeningEP.happening.crazyType == CrazyHappeningType.yRotation) ToggleYRotation(happeningEP.isOn);
    }

    private void FixedUpdate()
    {
        if (resetingRotation) ResetRotation();
        if (resetingScale)
        {
            ResetScale();
        }
        if (growingScale)
        {
            GrowScale();
        }
    }

    public void ToggleXZRotation(bool onOff)
    {
        if (onOff)
        {
            resetingRotation = false;
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationY;
        }
        else
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

    public void ToggleScale(bool onOff)
    {
        if (onOff)
        {
            resetingScale = false;
            growingScale = true;
        }
        else
        {
            resetingScale = true;
            growingScale = false;
        }
    }

    private void ResetScale()
    {
        if (transform.localScale.magnitude <= Vector3.one.magnitude)
        {
            transform.localScale = Vector3.one;
            resetingScale = false;
            growingScale = false;
            return;
        }
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, scaleSmooth);
    }

    private void GrowScale()
    {
        Vector3 target = Vector3.one * maxScaleMultiplier;
        if (transform.localScale.magnitude >= target.magnitude)
        {
            transform.localScale = target;
            growingScale = false;
            return;
        }
        transform.localScale = Vector3.Lerp(transform.localScale, target, scaleSmooth);
    }

    private void ResetRotation()
    {
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), 0.1f));

        if (Vector3.Angle(transform.up, Vector3.up) <= 3)
        {
            rb.MoveRotation(Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));
            resetingRotation = false;
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
}
