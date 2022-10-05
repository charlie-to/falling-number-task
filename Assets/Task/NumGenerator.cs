using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumGenerator : MonoBehaviour
{
    public string generateString(int digits)
    {
        int num = RandamGenerate(digits);
        return num.ToString();
    }

    public int RandamGenerate(int digits)
    {
        var rand = new System.Random();

        int minNum = (int) System.Math.Pow(10, digits - 1);
        int maxNum = (int) System.Math.Pow(10,digits) - 1;
        return rand.Next( minNum, maxNum );
    }
}
