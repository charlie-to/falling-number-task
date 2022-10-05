using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordDisplay : MonoBehaviour
{
    public Text text;
    public static float fallSpeed { get; protected set; }

    public void SetNum(string num)
    {
        text.text = num;
    }

    public void SetActive()
    {
        text.color = Color.red;
    }

    public void RemoveLetter()
    {
        text.text = text.text.Remove(0, 1);
    }

    public void RemoveNum()
    {
        Destroy(gameObject);
    }

    public void Start()
    {
        // init falling speed
        fallSpeed = TaskManegerInTask.FallingSpeed;
    }

    public void SetFallSpeed(float newFallSpeed)
    {
        fallSpeed = newFallSpeed;
    }

    private void Update()
    {
        transform.Translate(0f, -fallSpeed * Time.deltaTime, 0f);
    }
}
