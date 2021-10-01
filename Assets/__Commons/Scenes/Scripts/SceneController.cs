using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject loadingScreen;
    //public SceneSwitchEvent sceneSwitchEvent;
    public GameEventWithParam<SceneChangedEP> sceneChangedEvent;

    public LoadSceneEP loadFirst;
    public LoadSceneEP[] loadAfter;

    private Dictionary<SceneEnum, LoadSceneEP> currentLoadings = new Dictionary<SceneEnum, LoadSceneEP>();

    private void Awake()
    {
        //var sharedDataParam = new LoadSceneEP(SceneEnum.SHARED_DATA, SceneEnum.NULL, false, SharedDataLoaded);
        loadFirst.onLoad = FirstSceneLoaded;
        LoadScene(loadFirst);

        DeferIosGestures();
    }

    private void FirstSceneLoaded()
    {
        foreach(LoadSceneEP p in loadAfter)
        {
            LoadScene(p);
        }

        /*
        var titleParam = new LoadSceneEP(SceneEnum.TITLE, SceneEnum.NULL, false, null, null, true);
        LoadScene(titleParam);
        var musicParam = new LoadSceneEP(SceneEnum.MUSIC, SceneEnum.NULL, true);
        LoadScene(musicParam);
        */
    }

    public void LoadScene(LoadSceneEP param)
    {
        SetLoadingScreenActive(true);
        currentLoadings.Add(param.scene, param);
        var switchParam = new SceneChangedEP(SceneChangedEP.SceneLoadStateEnum.STARTED, param.scene, param.sceneToUnload);
        sceneChangedEvent.Raise(switchParam);
        StartCoroutine(SwitchSceneCoroutine(param));
    }

    // Should be executed next frame!
    private IEnumerator SwitchSceneCoroutine(LoadSceneEP param)
    {
        yield return null;
        if (param.sceneToUnload != SceneEnum.NULL) {
            SceneManager.UnloadSceneAsync((int)param.sceneToUnload);
        }
        var operation = SceneManager.LoadSceneAsync((int)param.scene, LoadSceneMode.Additive);
        operation.completed += (op) => UnitySceneLoaded(param);
    }

    public void SetLoadProgress(SceneLoadingProgressEP param)
    {
        if (param.complete)
        {
            SceneLoadCompleted(currentLoadings[param.scene]);
        }
    }

    private void UnitySceneLoaded(LoadSceneEP param)
    {
        PassParamToReceivers(param);
        if (param.showAfterLoad)
        {
            SceneLoadCompleted(param);
        }
        if (param.active) {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)param.scene));
        }
    }

    private void SceneLoadCompleted(LoadSceneEP param)
    {
        var switchParam = new SceneChangedEP(SceneChangedEP.SceneLoadStateEnum.LOADED, param.scene, param.sceneToUnload);
        sceneChangedEvent.Raise(switchParam);
        currentLoadings.Remove(param.scene);
        if (param.onLoad != null)
        {
            param.onLoad();
        }
        if (currentLoadings.Count == 0) {
            SetLoadingScreenActive(false);
        }
    }

    private void SetLoadingScreenActive(bool active)
    {
        loadingScreen.SetActive(active);
    }

    private void PassParamToReceivers(LoadSceneEP param)
    {
        var scene = SceneManager.GetSceneByBuildIndex((int) param.scene);
        foreach (GameObject obj in scene.GetRootGameObjects())
        {
            foreach (SceneParameterReceiver receiver in obj.GetComponentsInChildren<SceneParameterReceiver>())
            {
                receiver.PassParam(param.param);
            }
        }
    }

    private void DeferIosGestures()
    {
#if UNITY_IOS
        UnityEngine.iOS.Device.deferSystemGesturesMode = UnityEngine.iOS.SystemGestureDeferMode.All;
#endif
    }
}
