using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SlidingTextController : MonoBehaviour
{
    [SerializeField]
    private Color positiveColor = Color.green;
    [SerializeField]
    private Color negativeColor = Color.red;
    [SerializeField]
    private float movementSpeed = 30f;
    [SerializeField]
    private float lifeTime = 2f;
    [SerializeField]
    private float startDownPos = 5f;

    private float startTime;
    private float endTime;

    private Text text;

    private void Start()
    {
        startTime = Time.time;
        endTime = startTime + lifeTime;
        text = GetComponent<Text>();
        transform.position += Vector3.down * startDownPos;
    }

    private void Update()
    {
        transform.position += Vector3.down * movementSpeed * Time.deltaTime;
        text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.InverseLerp(endTime, startTime, Time.time));
        if (Time.time > endTime)
        {
            Destroy(gameObject);
        }
    }

    public void Init(bool positive, string textString)
    {
        text = GetComponent<Text>();
        text.color = positive ? positiveColor : negativeColor;
        text.text = textString;
    }
}
