using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskParameterChange : MonoBehaviour
{
    public TaskManager taskManager;
    public TMP_InputField fallingSpeedInput;
    public TextMeshProUGUI worningField;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Current Falling Speed:"+TaskManager.FallingSpeed.ToString());
        fallingSpeedInput.text = TaskManager.FallingSpeed.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickSubmit()
    {
        string newFallingSpeedText = fallingSpeedInput.text;
        decimal newFallingSpeedDecimal = 0;
        if (!decimal.TryParse(newFallingSpeedText, out newFallingSpeedDecimal))
        {
            worningField.text = "Try other Input Number...";
        }
        Debug.Log("Current Falling Speed:" + TaskManager.FallingSpeed);
        TaskManager.FallingSpeed = (float)newFallingSpeedDecimal;
        Debug.Log("Change Falling Speed:" + TaskManager.FallingSpeed);
    }
}
