using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    // To use interface need to make custom PropertyDrawer
    public MonoBehaviour[] loadables;
    public SceneEnum sceneIndex;
    //public SceneLoadingProgressEvent loadProgressSceneEvent;
    public GameEventWithParam<SceneLoadingProgressEP> loadProgressSceneEvent;

    private int costLoaded;
    private int sumCost;

    private void Start()
    {
        // Must be in different loops
        CalcOverallCost();
        StartLoading();
    }

    private void CalcOverallCost()
    {
        foreach (Loadable part in loadables)
        {
            sumCost += part.loadCost;
        }
        costLoaded = sumCost;
    }

    private void StartLoading()
    {
        if (loadables.Length == 0)
        {
            PartLoaded(0);
            return;
        }

        foreach (Loadable part in loadables)
        {
            part.Load(() => PartLoaded(part.loadCost));
        }
    }

    private void PartLoaded(int cost)
    {
        costLoaded += cost;
        var param = new SceneLoadingProgressEP(sceneIndex, costLoaded == sumCost, CalcLoadPercent());
        loadProgressSceneEvent.Raise(param);
    }

    private float CalcLoadPercent()
    {
        return sumCost == 0f ? 100f : (costLoaded / sumCost) * 100f;
    }
}
