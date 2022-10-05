using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NumberManager : MonoBehaviour
{
    public List<Number> numbers;

    public NumSpawmer numSpawmer;

    public TaskManegerInTask taskManeger;

    private Number activeNum;
    

    public void AddNumber( string inputNum)
    {
        Number number = new Number(inputNum, numSpawmer.SpawnNum() );
        numbers.Add(number);
        taskManeger.AddTaskEvent(EventType.NumberCreate);  // LogEvent
        SetActiveNum();
    }

    public void TypeLetter(char letter)
    {
        // Check if letter was next
        // remive it from nember
        if(activeNum.GetNextLetter() == letter)
        {
            activeNum.TypeLetter();
            taskManeger.AddTaskEvent(EventType.TypeCorrect); // LogEvent
        }
        else
        {
            Debug.Log("miss!");
            taskManeger.AddTaskEvent(EventType.TypeWrong); // LogEvent
        }

        if(activeNum.NumTyped())
        {
            taskManeger.AddTaskEvent(EventType.NumberDestroy, 7.0f - activeNum.GetNumberPosition()); //LogEvent
            numbers.Remove(activeNum);
            
            SetActiveNum();
        }
    }

    public void GameOverCheck()
    {
        // Numberが未登録ならゲーム続行
        if (numbers.Count == 0) return;

        Number number = numbers[0];

        // numberのy座標が範囲外ならゲームオーバー
        if (number.display.transform.position.y <= -6f)
        {
            //game over 処理
            taskManeger.Gameover();
        }
     
    }
    public void SetActiveNum()
    {
        // numbers に数字なければ何もしない
        if(numbers.Count == 0) return;

        activeNum = numbers[0];
        activeNum.display.SetActive();
    }
}
