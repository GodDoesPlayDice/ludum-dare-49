using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HappeningEmitter : MonoBehaviour
{
    private class HappeningHolder
    {
        public HappeningEntity happening;
        public HappeningType type;
        
        public HappeningHolder(HappeningEntity happening, HappeningType type)
        {
            this.happening = happening;
            this.type = type;
        }
        
    }

    [SerializeField]
    private GameEventWithParam<HappeningEP> happeningEvent;

    [SerializeField]
    private Vector2 emissionInterval = new Vector2(10.5f, 15f);

    [SerializeField]
    private Vector2 durationInterval = new Vector2(5f, 10f);

    //private List<HappeningEntity> crazyHappenings;
    //private List<HappeningEntity> stockHappenings;
    private List<HappeningHolder> happenings;

    private float nextCrazyTime;
    private float crazyStopTime;
    //private HappeningEntity crazyRunning;
    private HappeningHolder happeningRunning;

    //private float nextStockTime;
    //private float stockStopTime;
    //private HappeningEntity stockRunning;

    void Start()
    {
        happenings = Resources.LoadAll<HappeningEntity>("Crazy").Select(v => new HappeningHolder(v, HappeningType.CRAZY)).ToList();
        happenings.AddRange(Resources.LoadAll<HappeningEntity>("Stock").Select(v => new HappeningHolder(v, HappeningType.STOCK)));
        //stockHappenings = Resources.LoadAll<HappeningEntity>("Stock").ToList();
        nextCrazyTime = Random.Range(emissionInterval.x, emissionInterval.y);
        //nextStockTime = Random.Range(emissionInterval.x, emissionInterval.y);
        Debug.Log("Loaded happenings: " + happenings.Count);
    }

    // Now uses one list !!
    private void Update()
    {
        CheckAndExecuteCrazy();
        CheckAndStopCrazy();
    }

    private void CheckAndExecuteCrazy()
    {
        if (Time.time > nextCrazyTime)
        {
            //Debug.Log("Started");
            var holder = happenings[Random.Range(0, happenings.Count)];
            happeningEvent.Raise(new HappeningEP(holder.type, holder.happening, true));
            nextCrazyTime = Time.time + Random.Range(emissionInterval.x, emissionInterval.y);
            crazyStopTime = Time.time + Random.Range(durationInterval.x, durationInterval.y);
            happeningRunning = holder;
        }
    }

    private void CheckAndStopCrazy()
    {
        if (happeningRunning != null && Time.time > crazyStopTime)
        {
            //Debug.Log("Stopped");
            happeningEvent.Raise(new HappeningEP(happeningRunning.type, happeningRunning.happening, false));
            happeningRunning = null;
        }
    }

    /*
    private void CheckAndExecuteStock()
    {
        if (Time.time > nextCrazyTime)
        {
            var happening = crazyHappenings[Random.Range(0, crazyHappenings.Count)];
            happeningEvent.Raise(new HappeningEP(HappeningType.STOCK, happening, true));
            nextCrazyTime = Random.Range(emissionInterval.x, emissionInterval.y);
            crazyStopTime = Random.Range(durationInterval.x, durationInterval.y);
            crazyRunning = happening;
        }
    }

    private void CheckAndStopStock()
    {
        if (crazyRunning != null && Time.time > crazyStopTime)
        {
            happeningEvent.Raise(new HappeningEP(HappeningType.CRAZY, crazyRunning, false));
            crazyRunning = null;
        }
    }
    */
}
