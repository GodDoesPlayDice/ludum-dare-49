using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HappeningEmitter : MonoBehaviour
{
    [SerializeField]
    private GameEventWithParam<HappeningEP> happeningEvent;

    [SerializeField]
    private Vector2 emissionInterval = new Vector2(10.5f, 15f);

    [SerializeField]
    private Vector2 durationInterval = new Vector2(5f, 10f);

    private List<HappeningEntity> crazyHappenings;
    //private List<HappeningEntity> stockHappenings;

    private float nextCrazyTime;
    private float crazyStopTime;
    private HappeningEntity crazyRunning;

    //private float nextStockTime;
    //private float stockStopTime;
    //private HappeningEntity stockRunning;

    void Start()
    {
        crazyHappenings = Resources.LoadAll<HappeningEntity>("Crazy").ToList();
        crazyHappenings.AddRange(Resources.LoadAll<HappeningEntity>("Stock"));
        //stockHappenings = Resources.LoadAll<HappeningEntity>("Stock").ToList();
        nextCrazyTime = Random.Range(emissionInterval.x, emissionInterval.y);
        //nextStockTime = Random.Range(emissionInterval.x, emissionInterval.y);
        Debug.Log("Loaded happenings: " + crazyHappenings.Count);
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
            Debug.Log("Started");
            var happening = crazyHappenings[Random.Range(0, crazyHappenings.Count)];
            happeningEvent.Raise(new HappeningEP(HappeningType.CRAZY, happening, true));
            nextCrazyTime = Time.time + Random.Range(emissionInterval.x, emissionInterval.y);
            crazyStopTime = Time.time + Random.Range(durationInterval.x, durationInterval.y);
            crazyRunning = happening;
        }
    }

    private void CheckAndStopCrazy()
    {
        if (crazyRunning != null && Time.time > crazyStopTime)
        {
            Debug.Log("Stopped");
            happeningEvent.Raise(new HappeningEP(HappeningType.CRAZY, crazyRunning, false));
            crazyRunning = null;
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
