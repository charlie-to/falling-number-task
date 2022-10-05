using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class TaskStartBotunAction : MonoBehaviour
{
    public TMP_InputField fallingSpeed;
    public TMP_InputField delayTime;
    public TextMeshProUGUI fallingSpeedWrongWorning;
    public TaskManagerInTaskChoice taskManagerInTaskChoice;

    private float fallingSpeedNum;
    private float delayTimeNum;

    public void OnClick()
    {
        fallingSpeed = fallingSpeed.GetComponent<TMP_InputField>();
        delayTime = delayTime.GetComponent<TMP_InputField>();

        if (fallingSpeed.text == "")
        {
            fallingSpeedWrongWorning.text += "落下速度を入力してください。";
            StartCoroutine("TextSet");
            return;
        }
        if(delayTime.text == "")
        {
            fallingSpeedWrongWorning.text += "出現間隔を入力してください。";
        }

        try
        {
            fallingSpeedNum = Convert.ToSingle(fallingSpeed.text);
            delayTimeNum = Convert.ToSingle(delayTime.text);
        }
        catch
        {
            fallingSpeedWrongWorning.text += "不正な値です。";
            StartCoroutine("TextSet");
            return;
        }

        GameSceneToTask();
    }

    // ルーチン
    IEnumerator TextSet()
    {
        yield return new WaitForSeconds(5.0f);
        fallingSpeedWrongWorning.text = "";
    }

    private void GameSceneToTask()
    {
        Debug.Log("hello");
        Debug.Log(TaskManegerInTask.SubjectNum);
        SceneManager.sceneLoaded += GameSceneLoaded1;
        SceneManager.LoadScene("TaskScene");
    }

    private void GameSceneLoaded1(Scene next, LoadSceneMode mode)
    {
        // var nextTaskManager = GameObject.Find("TaskManager").GetComponent<TaskManegerInTask>();

        TaskManager.FallingSpeed = fallingSpeedNum;
        TaskManager.NumberSpawnDelayTime = delayTimeNum;

        SceneManager.sceneLoaded -= GameSceneLoaded1;
    }
}
