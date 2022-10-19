using Assets.Scripts.LoadSenarios;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class NumberTimer : MonoBehaviour
{
    public float numDelay;
    private float nextNumTime = 0f;

    public NumberManager numberManager;
    public NumDespawner numDespawner;
    public NumGenerator numGenerator;
    public PauseDisplay pauseDisplay;
    public TaskManegerInTask taskManeger;
    public InternalTimer timer;
    // private NumberSpawnDelayUpdate numberSpawnDelayUpdate;

    private SenarioTomlRepo senarioRepo;
    private Senario senario;

    private void Start()
    {
        // time freeze at beginning
        TimeFreeze();
        // numberSpawnDelayUpdate = new NumberSpawnDelayUpdate(timer);
        senarioRepo = new SenarioTomlRepo();
        senario = senarioRepo.GetSenario(TaskManegerInTask.SenarioNumber);
        TaskManager.NumberSpawnDelayTime = senario.NumberSpawnDelayTimeInstractions[0].NumberSpawnDelayTime;
        TaskManager.FallingSpeed = senario.FallingSpeed;
        TaskManager.Life = senario.LifeNumber;
        TaskManager.NumberOfDeleteOnDecLife = senario.NumberOfDeleteOnDecreaseLife;
    }

    private void Update()
    {
        // numberSpawnDelayUpdate.Update();
        if(Time.timeScale == 1)
        {
            TaskManegerInTask.NumberSpawnDelayTime = senario.GetSpawnDelayTimeByTime( timer.GetElapsedTime() );
        }
        //Debug.Log(TaskManager.NumberSpawnDelayTime);
        numberManager.GameOverCheck(); 
        NumberSpawn();
    }

    // 
    private void NumberSpawn()
    {
        if (Time.time >= nextNumTime)
         {
            numberManager.AddNumber( numGenerator.generateString(senario.digits) );
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
