using Assets.Scripts.LoadSenarios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static string Name = "default";
    public static string SubjectNum = "0000";
    public static float FallingSpeed = 1f;
    public static float NumberSpawnDelayTime = -1f;
    public static int SenarioNumber = 0;
    public static int Life = 3;
    public static int NumberOfDeleteOnDecLife = 3;
    public static float  RangeOfDeleteOnDecreaseLife = 0;

    public static float UserTypingSpeed_Max = 3.0f;
    public static SenarioType senarioType;
}
