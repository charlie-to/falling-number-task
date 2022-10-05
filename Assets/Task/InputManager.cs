using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public NumberManager numbnerManager;
    public NumberTimer numberTimer;

    private void Start()
    {
        // IMEを無効化
        Input.imeCompositionMode = IMECompositionMode.Off;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            numberTimer.TimeFreeze();
            return;
        }
        foreach(char letter in Input.inputString)
        {
            numbnerManager.TypeLetter(letter);
            using (StreamWriter streamWriter = new StreamWriter(Directory.GetCurrentDirectory() + "/LOGS.txt", true))
            {
                streamWriter.WriteLine(letter);
                streamWriter.Flush();
            }
        }
    }
}
