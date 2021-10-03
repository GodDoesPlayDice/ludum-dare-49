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

        var currentFullValue = CalcCurrentFullValue();
        if (currentFullValue >= 0)
        {
            currentValue = CalcCurrentFullValue();
        }
        else
        {
            ticksToNextChange = 0;
        }
        resourceChangedEvent.Raise(new ResourceChangedEP(currentValue));
    }

    public ResourceType GetResourceType()
    {
        return type;
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
