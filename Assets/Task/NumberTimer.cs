using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NumberTimer : MonoBehaviour
{
    public float numDelay;
    private float nextNumTime = 0f;
    private int digits = 3;

    public NumberManager numberManager;
    public NumDespawner numDespawner;
    public NumGenerator numGenerator;
    public PauseDisplay pauseDisplay;
    public TaskManegerInTask taskManeger;
    public InternalTimer timer;

    private NumberSpawnDelayUpdate numberSpawnDelayUpdate;

    private LoadSenario loadSenario = new LoadSenario();

    private void Start()
    {
        // time freeze at beginning
        TimeFreeze();
        numberSpawnDelayUpdate = new NumberSpawnDelayUpdate(timer);
        loadSenario.Load();
        Debug.Log("delaytime of toml :" + loadSenario.GetSpawnDelayTime());
    }

    private void Update()
    {
        numberSpawnDelayUpdate.Update();
        NumberSpawn();
        numberManager.GameOverCheck();
        Debug.Log("Delay:"+TaskManegerInTask.NumberSpawnDelayTime);
    }

    // 
    private void NumberSpawn()
    {
        if (Time.time >= nextNumTime)
         {
            numberManager.AddNumber( numGenerator.generateString(digits) );
            nextNumTime = Time.time + TaskManegerInTask.NumberSpawnDelayTime;
        }
    }

    public void TimeFreeze()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        pauseDisplay.PauseMessageChange();

        taskManeger.AddTaskEvent(EventType.TimePause);
    }
}
