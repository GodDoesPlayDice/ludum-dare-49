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
            portfolioChangedEvent.Raise(new PortfolioChangedEP(h.GetResourceType(), true, 0, 0, h.GetValue(), money));
        }


    }

    public void HandleWorkerOperation(WorkerOperationEP operation)
    {
        var holder = holdersMap[operation.type];
        var moneyCost = CalcMoneyCost(operation.type, Mathf.Abs(operation.resourceCount));

        PortfolioChangedEP param;

        if (operation.resourceCount < 0)
        {
            param = SellResourceAndGetEvent(holder, Mathf.Abs(operation.resourceCount), moneyCost);
        } else
        {
            param = BuyResourceAndGetEvent(holder, Mathf.Abs(operation.resourceCount), moneyCost);
        }
        portfolioChangedEvent.Raise(param);
    }

    private PortfolioChangedEP SellResourceAndGetEvent(ResourceHolder holder, int resourceCount, int moneyCost)
    {
        PortfolioChangedEP result;
        var forRemoval = holder.HowManyCanRemove(resourceCount);
        if (forRemoval == resourceCount)
        {
            //Debug.Log("Sell, true " + holder.GetValue());
            holder.Remove(resourceCount);
            //Debug.Log("After " + holder.GetValue());
            money += moneyCost;
            result = new PortfolioChangedEP(holder.GetResourceType(), true, forRemoval * -1, moneyCost, holder.GetValue(), money);
        } else
        {
            //Debug.Log("Sell, false");
            //money -= moneyCost;
            result = new PortfolioChangedEP(holder.GetResourceType(), false, 0, 0, holder.GetValue(), money);
        }
        return result;
    }

    private PortfolioChangedEP BuyResourceAndGetEvent(ResourceHolder holder, int resourceCount, int moneyCost)
    {
        PortfolioChangedEP result;
        if (money >= moneyCost)
        {
            //Debug.Log("Buy, true");
            holder.Add(resourceCount);
            money -= moneyCost;
            result = new PortfolioChangedEP(holder.GetResourceType(), true, resourceCount, moneyCost * -1, holder.GetValue(), money);
        } else
        {
            //Debug.Log("Buy, false");
            //var resourceRemoved = holder.Remove(resourceCount);
            result = new PortfolioChangedEP(holder.GetResourceType(), false, 0, 0, holder.GetValue(), money);
        }
        return result;
    }

    private int CalcMoneyCost(ResourceType type, int count)
    {
        return Mathf.RoundToInt(economics.GetCurrentCost(type)) * count;
    }
}
