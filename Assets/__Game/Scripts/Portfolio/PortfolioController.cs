using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortfolioController : MonoBehaviour
{
    [SerializeField]
    private int winCondition = 1000000;
    [SerializeField]
    private int money;
    [SerializeField]
    private List<ResourceHolder> resourceHolders;
    [SerializeField]
    private EconomicsController economics;
    [SerializeField]
    private GameEventWithParam<PortfolioChangedEP> portfolioChangedEvent;
    [SerializeField]
    private GameOverEvent gameOverEvent;

    [SerializeField]
    private AudioSource buyAudioSource;
    [SerializeField]
    private AudioSource sellAudioSource;
    [SerializeField]
    private AudioSource noMoneyAudioSource;


    private Dictionary<ResourceType, ResourceHolder> holdersMap;

    private bool won;

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
        if (money < 0)
        {
            gameOverEvent.Raise(new GameOverEP(false, "You are bankrupt!", "Tax office took all your investments, your house and your tie to pay off a debt.\n You should pay taxes on time!"));
        } else if (money >= winCondition && !won)
        {
            gameOverEvent.Raise(new GameOverEP(true, "You won!", "Now you can continue to earn all money in the world!"));
        }
    }

    public int GetCurrentMoney()
    {
        return money;
    }

    public int GetCurrentResource(ResourceType type)
    {
        return holdersMap[type].GetValue();
    }

    public void SetWon(bool won)
    {
        this.won = won;
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
