using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortfolioController : MonoBehaviour
{
    [SerializeField]
    private int money;
    [SerializeField]
    private List<ResourceHolder> resourceHolders;
    [SerializeField]
    private EconomicsController economics;
    [SerializeField]
    private GameEventWithParam<PortfolioChangedEP> portfolioChangedEvent;

    private Dictionary<ResourceType, ResourceHolder> holdersMap;

    private void Start()
    {
        holdersMap = new Dictionary<ResourceType, ResourceHolder>();
        foreach (ResourceHolder h in resourceHolders)
        {
            holdersMap.Add(h.GetResourceType(), h);
        }
    }

    public void HandleWorkerOperation(WorkerOperationEP operation)
    {
        var holder = holdersMap[operation.type];
        var moneyCost = CalcMoneyCost(operation.type, Mathf.Abs(operation.resourceCount));

        PortfolioChangedEP param;
        if (operation.resourceCount < 0)
        {
            param = SellResourceAndGetEvent(holder, operation.resourceCount, moneyCost);
        } else
        {
            param = BuyResourceAndGetEvent(holder, operation.resourceCount, moneyCost);
        }
        portfolioChangedEvent.Raise(param);
    }

    private PortfolioChangedEP SellResourceAndGetEvent(ResourceHolder holder, int resourceCount, int moneyCost)
    {
        PortfolioChangedEP result;
        var forRemoval = holder.HowManyCanRemove(resourceCount);
        if (forRemoval == resourceCount)
        {
            holder.Remove(resourceCount);
            money += moneyCost;
            result = new PortfolioChangedEP(holder.GetResourceType(), true, forRemoval * -1, moneyCost, holder.GetValue(), money);
        } else
        {
            money -= moneyCost;
            result = new PortfolioChangedEP(holder.GetResourceType(), false, 0, moneyCost * -1, holder.GetValue(), money);
        }
        return result;
    }

    private PortfolioChangedEP BuyResourceAndGetEvent(ResourceHolder holder, int resourceCount, int moneyCost)
    {
        PortfolioChangedEP result;
        if (money >= moneyCost)
        {
            holder.Add(resourceCount);
            money -= moneyCost;
            result = new PortfolioChangedEP(holder.GetResourceType(), true, resourceCount, moneyCost * -1, holder.GetValue(), money);
        } else
        {
            var resourceRemoved = holder.Remove(resourceCount);
            result = new PortfolioChangedEP(holder.GetResourceType(), false, resourceRemoved, 0, holder.GetValue(), money);
        }
        return result;
    }

    private int CalcMoneyCost(ResourceType type, int count)
    {
        return Mathf.RoundToInt(economics.GetCurrentCost(type)) * count;
    }
}
