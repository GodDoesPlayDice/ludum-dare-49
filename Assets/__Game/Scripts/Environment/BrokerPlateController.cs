using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using System.Globalization;

public class BrokerPlateController : MonoBehaviour
{
    public GameObject resourceText;
    public GameObject costText;
    public GameObject[] buttons;

    private AimConstraint aimConstraint;
    private Transform cameraHolder;
    private TextMesh costTextMesh;
    private void Start()
    {
        resourceText.SetActive(true);
        aimConstraint = GetComponent<AimConstraint>();
        cameraHolder = GameObject.FindGameObjectWithTag("CameraHolder").transform;

        costTextMesh = costText.GetComponent<TextMesh>();
        if (aimConstraint != null)
        {
            aimConstraint.constraintActive = true;
            ConstraintSource constraintSource = new ConstraintSource();
            constraintSource.sourceTransform = cameraHolder;
            constraintSource.weight = 1;
            aimConstraint.AddSource(constraintSource);
        }
    }

    public void OnCostChange(ResourceChangedEP resourceChangedEP) 
    {
        costTextMesh.text = formatMoney((int)resourceChangedEP.value);
    }

    public void ShowButtons(bool needToShow)
    {
        if (needToShow)
        {
            foreach (GameObject button in buttons)
            {
                if (!button.activeSelf) button.SetActive(true);
            }
        } else
        {
            foreach (GameObject button in buttons)
            {
                if (button.activeSelf) button.SetActive(false);
            }
        }
    }

    private string formatMoney(int money)
    {
        var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        nfi.NumberGroupSeparator = " ";
        return money.ToString("#,0.00 $", nfi);
    }
}
