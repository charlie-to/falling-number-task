using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using UnityEngine.SceneManagement;

public class TaskManegerInTask : TaskManager
{
    private string ResultSaveDirectory = "TaskResultData";
    [SerializeField]
    private TaskData taskData;

    private void Start()
    {
        PropatyInitiate();
        //Debug.Log("this is when start of tm");
        //Debug.Log("subject Num : " + TaskManegerInTask.SubjectNum);
        //Debug.Log("falling speed:" + TaskManegerInTask.FallingSpeed);
        taskData = new TaskData(SubjectNum, FallingSpeed, NumberSpawnDelayTime);
    }

    // Falling speed が初期値の場合は１を代入
    private void PropatyInitiate() { 
        if(FallingSpeed == 0)
        {
            FallingSpeed = 0.7f;
        }
        if (SubjectNum == null)
        {
            SubjectNum = "0000";
        }
        if (NumberSpawnDelayTime == 0) NumberSpawnDelayTime = 3.0f;
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
            SceneManager.LoadScene("TaskChoiceScene");
        } catch (Exception e)
        {
            // ファイルを作成できなかったときになにかする
            ErrorDialogMessageHandlerField.ErrorMessageText = "cannnot write json to a file. the json is " + JsonUtility.ToJson(taskData) + e.Message;
            SceneManager.LoadScene("ErrorDialogScene");
        }
        
    }
}
