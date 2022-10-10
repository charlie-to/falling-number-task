using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginBottunAction : MonoBehaviour
{
    public TMP_InputField subjectNumberField;
    public TextMeshProUGUI wrongInputWorning;

    private CheckDigit CheckDigit = new CheckDigit();

    public void OnClick()
    {
        subjectNumberField = subjectNumberField.GetComponent<TMP_InputField>();

        if( CheckDigit.IsCorrectCheckDigit( subjectNumberField.text ) )
        {
            ChangeSceneToTask();
        }
        else
        {
            wrongInputWorning.text = "入力に誤りがあります";
            StartCoroutine("TextSet");
        }
    }

    private void ChangeSceneToTask()
    {
        SceneManager.sceneLoaded += GameSceneLoaded;
        SceneManager.LoadScene("TaskChoiceScene");
    }

    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // var nextTaskManager = GameObject.Find("TaskManager").GetComponent<TaskManagerInTaskChoice>();

        TaskManager.SubjectNum = subjectNumberField.text;

        SceneManager.sceneLoaded -= GameSceneLoaded;
    }

    // ルーチン
    IEnumerator TextSet()
    {
        yield return new WaitForSeconds(5.0f);
        wrongInputWorning.text = "";
    }
}
