using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using UnityEngine.SceneManagement;
using Assets.Scripts.LoadSenarios;

public class TaskManegerInTask : TaskManager
{
    private string ResultSaveDirectory = $"TaskResultData/{TaskManager.SubjectNum}";
    [SerializeField]
    private TaskData taskData;

    private void Start()
    {
        taskData = new TaskData(TaskManager.Name, TaskManager.SubjectNum, TaskManager.FallingSpeed, TaskManager.NumberSpawnDelayTime, TaskManager.Life, TaskManager.NumberOfDeleteOnDecLife ,TaskManager.RangeOfDeleteOnDecreaseLife);
    }

    // タスクイベント
    public void AddTaskEvent(EventType _eventType)
    {
        taskData.AddTaskEvent(_eventType);
    }
    public void AddTaskEvent(EventType _eventType, float Position)
    {
        taskData.AddTaskEvent(_eventType, Position);
    }



    // ゲームオーバー処理
    public void Gameover()
    {
        var filePath = Directory.GetCurrentDirectory() + "/" + ResultSaveDirectory;
        Debug.Log(filePath);
        var fileName = SubjectNum + "_" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + "-" + DateTime.Now.ToString("HH") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss")+ "-" + TaskManager.SenarioNumber.ToString() + ".json";

        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

        try
        {
            //テスト用
            //
            //throw new Exception("test");
            //

            using (StreamWriter sw = new StreamWriter(filePath + "/" + fileName, false))
            {
                sw.WriteLine(JsonUtility.ToJson(taskData));
                sw.Flush();
            }
            Time.timeScale = 1;
            SceneManager.LoadScene("TaskChoiceScene");
        }
        catch (Exception e)
        {
            // ファイルを作成できなかったときになにかする
            ErrorDialogMessageHandlerField.ErrorMessageText = "cannnot write json to a file. the json is " + JsonUtility.ToJson(taskData) + e.Message;
            SceneManager.LoadScene("ErrorDialogScene");
        }

        if(senarioType == SenarioType.Measure)
        {
            TaskManager.UserTypingSpeed_Max = taskData.GetUserTypingTimedByArea(7f);
        }
        
    }
}
