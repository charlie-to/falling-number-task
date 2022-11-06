using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManagerInTaskChoice : TaskManager
{
    public TextMeshProUGUI subjectNumberField;


    private void Start()
    {
        subjectNumberField.text = TaskManagerInTaskChoice.SubjectNum;   
    }
}
