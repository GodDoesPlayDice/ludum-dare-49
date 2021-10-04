using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BrokerPlateController : MonoBehaviour
{
    public GameObject resourceText;
    public GameObject costText;

    private AimConstraint aimConstraint;
    private Transform cameraHolder;
    private void Start()
    {
        resourceText.SetActive(true);
        aimConstraint = GetComponent<AimConstraint>();
        cameraHolder = GameObject.FindGameObjectWithTag("CameraHolder").transform;

        if (aimConstraint != null)
        {
            aimConstraint.constraintActive = true;
            ConstraintSource constraintSource = new ConstraintSource();
            constraintSource.sourceTransform = cameraHolder;
            constraintSource.weight = 1;
            aimConstraint.AddSource(constraintSource);
        }

    }
}
