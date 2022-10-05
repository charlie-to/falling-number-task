using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManagerInTaskChoice : TaskManager
{
    public TextMeshProUGUI subjectNumberField;


    private void Start()
    {

        Debug.Log("this is start tm");
        Debug.Log(message: TaskManagerInTaskChoice.SubjectNum);

        subjectNumberField.text = TaskManagerInTaskChoice.SubjectNum;

        
    }
}
