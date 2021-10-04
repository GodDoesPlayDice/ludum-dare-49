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
            c.startValue = startValue;
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

    public float GetCurrentCost(ResourceType type)
    {
        return resourceControllers.Find(c => c.GetResourceType() == type).currentValue;
    }

    public float GetStartValue()
    {
        return startValue;
    }

    public void HandleHappening(HappeningEP param)
    {
        if (param.type != HappeningType.STOCK)
        {
            return;
        }

        if (param.isOn)
        {
            resourceControllers.FindAll(c => param.happening.resourceTypes.Contains(c.GetResourceType())).ForEach(c => c.SetCurrentHappening(param.happening));
        }
        else
        {
            resourceControllers.FindAll(c => param.happening.resourceTypes.Contains(c.GetResourceType())).ForEach(c => c.RemoveCurrentHappening());
        }
    }
}
