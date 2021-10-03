using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EconomicsController : MonoBehaviour
{
    [SerializeField]
    private float tickDeltaTime = 1f;

    [SerializeField]
    private int minTicksPeriod;

    [SerializeField]
    private int maxTicksPeriod;

    [SerializeField]
    private int maxChangePerTick;

    [SerializeField]
    private int maxAdditionalFluct;

    [SerializeField]
    private List<ResourceEconomicController> resourceControllers;

    private float prevTime;

    void Update()
    {
        if (Time.time - prevTime > tickDeltaTime)
        {
            prevTime = Time.time;
            resourceControllers.ForEach(c => c.NextEconomicsTick());
        }
    }
}
