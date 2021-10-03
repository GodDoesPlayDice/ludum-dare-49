using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceEconomicController : MonoBehaviour
{
    [SerializeField]
    private GameEventWithParam<ResourceChangedEP> resourceChangedEvent;

    [SerializeField]
    private ResourceType type;

    public float currentValue;

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
        //Debug.Log("Cur: " + currentValue);
        if (ticksToNextChange == 0) {
            CalcCurrentDelta();
            ticksToNextChange = Random.Range(periodRange.start, periodRange.end + 1);
        }
        ticksToNextChange--;

        currentValue = CalcCurrentFullValue();
        resourceChangedEvent.Raise(new ResourceChangedEP(currentValue));
    }

    // Full random in bounds
    private void CalcCurrentDelta()
    {
        var minBound = Mathf.Min(currentValue - minMaxRange.start, maxChangePerTick);
        var maxBound = Mathf.Min(minMaxRange.end - currentValue, maxChangePerTick);
        currentDeltaPerTick = Random.Range(-minBound, maxBound);
    }

    private float CalcCurrentFullValue()
    {
        return currentValue + currentDeltaPerTick + Random.Range(-maxAdditionalFluct, maxAdditionalFluct);
    }
}
