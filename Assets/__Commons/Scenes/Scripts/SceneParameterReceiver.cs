using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneParameterReceiver : MonoBehaviour
{
    public UnityEvent<string> response;

    public void PassParam(string param)
    {
        response.Invoke(param);
    }
}
