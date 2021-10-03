using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EconomicsController : MonoBehaviour
{
    [SerializeField]
    private float tickDeltaTime = 1f;

    [SerializeField]
    private float startValue = 10000f;

    [SerializeField]
    private int minBound = 1000;

    [SerializeField]
    private int maxBound = 20000;

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

    private void Start()
    {
        resourceControllers.ForEach(c => {
            c.currentValue = startValue;
            c.minMaxRange = new RangeInt(minBound, maxBound);
            c.periodRange = new RangeInt(minTicksPeriod, maxTicksPeriod);
            c.maxChangePerTick = maxChangePerTick;
            c.maxAdditionalFluct = maxAdditionalFluct;
        });
    }

    void Update()
    {
        if (Time.time - prevTime > tickDeltaTime)
        {
            prevTime = Time.time;
            resourceControllers.ForEach(c => c.NextEconomicsTick());
        }
    }
}
