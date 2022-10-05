using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface INumberSpawnDelayUpdate
{
    void Update();

    void SetSenario( int senarioId );
}

public class NumberSpawnDelayUpdate : INumberSpawnDelayUpdate
{
    private SenarioRepository senarioRepository;

    private int selectedSenarioNum = 0;
    private int eventNum = 0;
    private float duration = 0f;
    private InternalTimer timer;
    
    public void Update()
    {
        if(duration <= timer.GetElapsedTime() )
        {
            eventNum++;
            Debug.LogAssertion("Event Number : "+eventNum);
            duration = (float) senarioRepository.GetSenarioParam(selectedSenarioNum, eventNum).duration;

            timer.StartTimer();

        }

        if(Time.timeScale ==1)
        {
            var senarioType = senarioRepository.GetSenarioParam(selectedSenarioNum, eventNum).event_type;

            if ( senarioType == "stay") return;

            if (senarioType == "wlup")
            {
                var pace = (TaskManegerInTask.NumberSpawnDelayTime - senarioRepository.GetSenarioParam(selectedSenarioNum, eventNum).dest_delaytime) / (senarioRepository.GetSenarioParam(selectedSenarioNum, eventNum).duration);
                DecreaseNumberSpaenDelayTime((float) pace);
            }
        }

        

    }

    public void SetSenario(int senarioId)
    {
        if(senarioId > senarioRepository.GetSenarioListLength() ) selectedSenarioNum = 0;

        selectedSenarioNum = senarioId;
    }

    public NumberSpawnDelayUpdate(InternalTimer _timer)
    {
        senarioRepository = new SenarioRepository();
        timer = _timer;
    }
    
    private void UpdateNumberSpawnDelayTime(float newSpaenTime )
    {
        TaskManegerInTask.NumberSpawnDelayTime = newSpaenTime;
    }

    public void DecreaseNumberSpaenDelayTime(float decreaseTime)
    {
        float newSpawnDelayTime = TaskManegerInTask.NumberSpawnDelayTime - decreaseTime;
        UpdateNumberSpawnDelayTime(newSpawnDelayTime);
    }
}
