using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadingProgressEP : IGameEventParam
{
    public SceneEnum scene;
    public bool complete;
    public float loadPercent;

    public SceneLoadingProgressEP(SceneEnum scene, bool complete, float loadPercent = 0f)
    {
        this.scene = scene;
        this.complete = complete;
        this.loadPercent = loadPercent;
    }
}
