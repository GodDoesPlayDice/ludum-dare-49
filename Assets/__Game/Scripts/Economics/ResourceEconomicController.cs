using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceEconomicController : MonoBehaviour
{
    [SerializeField]
    private GameEventWithParam<ResourceChangedEP> resourceChangedEvent;

    [SerializeField]
    private ResourceType type;

    [SerializeField]
    private float currentValue = 100;

    [HideInInspector]
    public RangeInt minMaxRange;
    [HideInInspector]
    public RangeInt periodRange;
    [HideInInspector]
    public float maxChangePerTick;
    [HideInInspector]
    public float maxAdditionalFluct;

    private int ticksToNextChange;
    private float currentDeltaPerTick;

    public void NextEconomicsTick()
    {
        if (ticksToNextChange == 0) {
            CalcCurrentDelta();
            ticksToNextChange = Random.Range(periodRange.start, periodRange.end + 1);
        }

        currentValue = CalcCurrentFullValue();
        resourceChangedEvent.Raise(new ResourceChangedEP(currentValue));
    }

    // Full random in bounds
    private void CalcCurrentDelta()
    {
        var minBound = Mathf.Max(minMaxRange.start, currentValue - maxChangePerTick);
        var maxBound = Mathf.Max(minMaxRange.end, currentValue + maxChangePerTick);
        currentDeltaPerTick = Random.Range(minBound, maxBound);
    }

    private float CalcCurrentFullValue()
    {
        return currentDeltaPerTick + Random.Range(-maxAdditionalFluct, maxAdditionalFluct);
    }
}
