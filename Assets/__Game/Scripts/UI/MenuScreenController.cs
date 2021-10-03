using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreenController : MonoBehaviour
{
    [SerializeField]
    private GameEventWithParam<LoadSceneEP> loadSceneEvent;

    public void OnNewGameClick() {
        loadSceneEvent.Raise(new LoadSceneEP(SceneEnum.GAME, SceneEnum.TITLE, active: true));
    }
}
