using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Unity.VisualScripting;

public class TaskStartBotunAction : MonoBehaviour
{
    public TMP_InputField senarioNumberInputField;
    public TextMeshProUGUI fallingSpeedWrongWorning;
    public TextMeshProUGUI senarioLoadStatusField;

    [Serialize]
    private SenarioTomlRepo senarioTomlRepo;
    private int senarioNumber;

    public void Start()
    {
        LoadSenarios();
        StartCoroutine(TextSetNull());
    }

    public void LoadSenarios()
    {
        senarioTomlRepo = new SenarioTomlRepo();
        try
         {
            senarioLoadStatusField.text = $" {senarioTomlRepo.GetSenarioLength().ToString()} Senarios Loaded ";
         }
         catch
         {
            senarioLoadStatusField.text = "Senario data is missing.";
         }
    }

    public void OnClick()
    {
        
        senarioNumberInputField = senarioNumberInputField.GetComponent<TMP_InputField>();

        if (senarioNumberInputField.text == null)
        {
            fallingSpeedWrongWorning.text = "シナリオ番号を入力してください。"; 
            return;
        }

        try
        {
            senarioNumber = int.Parse(senarioNumberInputField.text);
        }
        catch
        {
            fallingSpeedWrongWorning.text = "不正な入力値です。";
            return;
        }

        if (senarioNumber <= 0 || senarioTomlRepo.GetSenarioLength() < senarioNumber)
        {
            fallingSpeedWrongWorning.text = "入力した値に対応するシナリオが存在しません";
            return ;
        }  

        GameSceneToTask();
    }

    public void OnClickReload()
    {
        LoadSenarios();
    }

    // ルーチン
    private IEnumerator TextSetNull()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            fallingSpeedWrongWorning.text = "";
        }
    }

    private void GameSceneToTask()
    {
        TaskManager.SenarioNumber = senarioNumber -1;
        SceneManager.LoadScene("TaskScene");
    }
}
