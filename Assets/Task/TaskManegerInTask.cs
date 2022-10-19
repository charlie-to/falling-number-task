using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using UnityEngine.SceneManagement;

public class TaskManegerInTask : TaskManager
{
    private string ResultSaveDirectory = $"TaskResultData\\{TaskManager.SubjectNum}";
    [SerializeField]
    private TaskData taskData;

    private void Start()
    {
        taskData = new TaskData(TaskManager.SubjectNum, TaskManager.FallingSpeed, TaskManager.NumberSpawnDelayTime, TaskManager.Life, TaskManager.NumberOfDeleteOnDecLife);
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

        var filePath = Directory.GetCurrentDirectory() + "\\" + ResultSaveDirectory;
        Debug.Log(filePath);
        var fileName = SubjectNum + "_" + DateTime.Now.ToString("D")+ "_" + DateTime.Now.ToString("HH")+ "時" + DateTime.Now.ToString("mm") + "分"+ DateTime.Now.ToString("ss")+ "秒" + ".json";

        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

        try
        {
            //テスト用
            //
            //throw new Exception("test");
            //

            using (StreamWriter sw = new StreamWriter(filePath + "\\" + fileName , false))
            {
                sw.WriteLine(JsonUtility.ToJson(taskData));
                sw.Flush();
            }
            Time.timeScale = 1;
            SceneManager.LoadScene("TaskChoiceScene");
        } catch (Exception e)
        {
            // ファイルを作成できなかったときになにかする
            ErrorDialogMessageHandlerField.ErrorMessageText = "cannnot write json to a file. the json is " + JsonUtility.ToJson(taskData) + e.Message;
            SceneManager.LoadScene("ErrorDialogScene");
        }
        
    }
}
