using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent eventForListen;
    public UnityEvent response;

    private void OnEnable()
    {
        eventForListen.AddListener(this);
    }

    private void OnDisable()
    {
        eventForListen.RemoveListener(this);
    }

    public void OnEventRaised()
    {
        response.Invoke();
    }
}
