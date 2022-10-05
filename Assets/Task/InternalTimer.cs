using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface IInternalTimer
{
    void StartTimer();
    void StopTimer();
    float GetElapsedTime();
}
public class InternalTimer :  MonoBehaviour, IInternalTimer
{

    public static InternalTimer instance;

    private float elapsedTime;
    private bool running;

    public bool IsRunning { get { return running; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        running = false;
        elapsedTime = 0;
    }
    private void Update()
    {
        if (Time.timeScale == 0) return;
        
        elapsedTime += Time.deltaTime;
    }
    public void StartTimer()
    {
        elapsedTime = 0;
        running = true;
    }

    public void StopTimer()
    {
        running = false;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
