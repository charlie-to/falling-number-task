using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Number
{
    public string number;
    private int typeIndex;

    public WordDisplay display;

    public Number( string _number, WordDisplay _display)
    {
        number = _number;
        typeIndex = 0;
        
        display = _display;
        display.SetNum(number);
    }

    public char? GetNextLetter()
    {
        try
        {
            return number[typeIndex];
        }
        catch
        {
            return null;
        }
    }

    public void TypeLetter()
    {
        typeIndex++;
        // remove letter from screen
        display.RemoveLetter();
    }

    public bool NumTyped()
    {
        bool numTyped = (typeIndex >= number.Length);

        if (numTyped)
        {
            NumRemove();   
        }

        return numTyped;
    }

    public void NumRemove()
    {
        display.RemoveNum();
    }

    public float GetNumberPosition()
    {
        return display.transform.position.y;
    }
}
