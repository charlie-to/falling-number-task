using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBotton : MonoBehaviour
{
    public TaskManegerInTask taskManeger;

    public void OnClick()
    {
        taskManeger.Gameover();
    }
}
