using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseDisplay : MonoBehaviour
{
    public TextMeshProUGUI pauseMessage;

    public void PauseMessageChange()
    {
        pauseMessage.text = (Time.timeScale) == 0 ? "スペースバーを押して開始" : "";
    }
}
