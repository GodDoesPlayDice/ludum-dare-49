using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverEP : IGameEventParam
{
    public bool success { get; private set; }
    public string title { get; private set; }
    public string description { get; private set; }

    public GameOverEP(bool success, string title, string description)
    {
        this.success = success;
        this.title = title;
        this.description = description;
    }
}
