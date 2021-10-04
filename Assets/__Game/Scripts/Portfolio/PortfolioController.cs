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

    [SerializeField]
    private AudioSource buyAudioSource;
    [SerializeField]
    private AudioSource sellAudioSource;
    [SerializeField]
    private AudioSource noMoneyAudioSource;


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

    // !!!!!
    public void RemoveTaxesMoney(int amount)
    {
        money -= amount;
    }

    public int GetCurrentMoney()
    {
        return money;
    }

    public int GetCurrentResource(ResourceType type)
    {
        return holdersMap[type].GetValue();
    }

    private PortfolioChangedEP SellResourceAndGetEvent(ResourceHolder holder, int resourceCount, int moneyCost)
    {
        PortfolioChangedEP result;
        var forRemoval = holder.HowManyCanRemove(resourceCount);
        if (forRemoval == resourceCount)
        {
            PlaySound(sellAudioSource);
            holder.Remove(resourceCount);
            money += moneyCost;
            result = new PortfolioChangedEP(holder.GetResourceType(), true, forRemoval * -1, moneyCost, holder.GetValue(), money);
        } else
        {
            PlaySound(noMoneyAudioSource);
            result = new PortfolioChangedEP(holder.GetResourceType(), false, 0, 0, holder.GetValue(), money);
        }
        return result;
    }

    private PortfolioChangedEP BuyResourceAndGetEvent(ResourceHolder holder, int resourceCount, int moneyCost)
    {
        PortfolioChangedEP result;
        if (money >= moneyCost)
        {
            PlaySound(buyAudioSource);
            holder.Add(resourceCount);
            money -= moneyCost;
            result = new PortfolioChangedEP(holder.GetResourceType(), true, resourceCount, moneyCost * -1, holder.GetValue(), money);
        } else
        {
            PlaySound(noMoneyAudioSource);
            result = new PortfolioChangedEP(holder.GetResourceType(), false, 0, 0, holder.GetValue(), money);
        }
        return result;
    }

    private int CalcMoneyCost(ResourceType type, int count)
    {
        return Mathf.RoundToInt(economics.GetCurrentCost(type)) * count;
    }


    private void PlaySound(AudioSource audioSource)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
