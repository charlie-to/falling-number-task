using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NumberManager : MonoBehaviour
{
    public List<Number> numbers;

    public NumSpawmer numSpawmer;

    public TaskManegerInTask taskManeger;

    private Number activeNum;


    public void AddNumber(string inputNum)
    {
        Number number = new Number(inputNum, numSpawmer.SpawnNum());
        numbers.Add(number);
        taskManeger.AddTaskEvent(EventType.NumberCreate);  // LogEvent
        SetActiveNum();
    }

    public void TypeLetter(char letter)
    {
        if (activeNum == null) { return; }

        // Check if letter was next
        // remive it from nember
        if (activeNum.GetNextLetter() == letter)
        {
            activeNum.TypeLetter();
            taskManeger.AddTaskEvent(EventType.TypeCorrect); // LogEvent
        }
        else
        {
            Debug.Log("miss!");
            taskManeger.AddTaskEvent(EventType.TypeWrong); // LogEvent
        }

        if (activeNum.NumTyped())
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

        // numberのy座標が範囲外ならライフを減らす
        if (numbers[0].display.transform.position.y <= -6f)
        {
            // ライフを減らす
            TaskManager.Life--;
            taskManeger.AddTaskEvent(EventType.DecreaseLife); // Log Event

            // 指定個数の数字を消す
            for (int i = 0; i <= TaskManager.NumberOfDeleteOnDecLife; i++)
            {
                if (numbers.Count == 0) break;
                if (numbers[0] != null)
                {
                    Number deleteNumber = numbers[0];
                    numbers.Remove(deleteNumber);
                    deleteNumber.NumRemove();
                }
            }
            // 指定範囲以下の数字を消す
            if (TaskManager.RangeOfDeleteOnDecreaseLife > 0)
            {
                List<Number> deleteNumbers = new List<Number>();
                foreach (Number number in numbers)
                {
                    if (7.0f - number.GetNumberPosition() >= TaskManager.RangeOfDeleteOnDecreaseLife)
                    {
                        deleteNumbers.Add(number);
                    }
                }
                foreach (Number deleteNumber in deleteNumbers)
                {
                    numbers.Remove(deleteNumber);
                    deleteNumber.NumRemove();
                }
            }

            SetActiveNum();

            // ライフ０ならゲームオーバー
            if (TaskManager.Life <= 0)
            {
                //game over 処理
                taskManeger.Gameover();
                numbers.Clear();
            }
        }

        // SpawnDelayTimeが負の大きい値ならゲームオーバー
        if (TaskManegerInTask.NumberSpawnDelayTime < 0)
        {
            taskManeger.Gameover();
            numbers.Clear();
        }
    }
    public void SetActiveNum()
    {
        // numbers に数字なければactiviNumはnull
        if (numbers.Count == 0)
        {
            activeNum = null;
            return;
        }

        activeNum = numbers[0];
        activeNum.display.SetActive();
    }
}
