using System;
using UnityEngine;

[Serializable]
public class LoadSceneEP : IGameEventParam
{
    // SerializeField can't work with private setters, so evetyting is public

    public SceneEnum scene;
    public SceneEnum sceneToUnload;
    public bool showAfterLoad;
    public Action onLoad;
    public string param;
    public bool active;

    public LoadSceneEP()
    {

    }

    public LoadSceneEP(SceneEnum scene, SceneEnum sceneToUnload = SceneEnum.NULL, bool showAfterLoad = true, Action onLoad = null, string param = null, bool active = false)
    {
        this.scene = scene;
        this.sceneToUnload = sceneToUnload;
        this.showAfterLoad = showAfterLoad;
        this.onLoad = onLoad;
        this.param = param;
        this.active = active;
    }
}
