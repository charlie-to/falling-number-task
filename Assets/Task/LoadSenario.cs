using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nett;
using System.IO;
using Unity.VisualScripting;

public class LoadSenario
{
    public string file_name;

    private TomlTable train1;

    public void Load()
    {
        // read input file
        //string input_file_path = Directory.GetCurrentDirectory() + directry_name + file_name;
        var toml_file = string.Format("{0}/../Senario.toml", Application.dataPath);
        TomlTable root = Toml.ReadFile(toml_file);
        TomlTable meta = root.Get<TomlTable>("meta");
        TomlTableArray Senarios = root.Get<TomlTableArray>("Senarios");
        train1 = Senarios[0];

        string name = meta.Get<string>("name");
        Debug.Log(name);
    }

    public float GetSpawnDelayTime()
    {
        return train1.Get<float>("SpawnDelayTime");
    }
}
