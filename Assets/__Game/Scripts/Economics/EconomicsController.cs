using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomicsController : MonoBehaviour
{
    [SerializeField]
    private GameEventWithParam<ResourceChangedEP> goldChangedEvent;
    [SerializeField]
    private GameEventWithParam<ResourceChangedEP> woodChangedEvent;
    [SerializeField]
    private GameEventWithParam<ResourceChangedEP> oilChangedEvent;

    private float prevTime;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - prevTime > 0.2)
        {
            prevTime = Time.time;
            goldChangedEvent.Raise(new ResourceChangedEP(Random.Range(0f, 400f)));
        }
    }


}
