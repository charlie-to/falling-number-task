using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugBotton : MonoBehaviour
{
    public TextMeshProUGUI worningText;

    private SenarioTomlRepo senarioTomlRepo;

    public void OnClick()
    {
        senarioTomlRepo = new SenarioTomlRepo();
        worningText.text = senarioTomlRepo.file_path;
    }
}
