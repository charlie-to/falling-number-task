using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nett;
using System.IO;
using Unity.VisualScripting;
using System;
using Assets.Scripts.LoadSenarios;

public class SenarioTomlRepo
{
    public string file_path = string.Format("{0}\\Senario.toml", Directory.GetCurrentDirectory());

    private TomlTableArray senarios;
    private TomlTable meta;

    /// <summary>
    /// �C���X�^���X�ɃV�i���I�f�[�^��������
    /// </summary>
    public SenarioTomlRepo()
    {
        this.Load();
    }


    public void Load()
    {
        try
        {
            TomlTable root = Toml.ReadFile(file_path);
            meta = root.Get<TomlTable>("meta");
            senarios = root.Get<TomlTableArray>("Senarios");
        }
        catch
        {
            return;
        }

        string name = meta.Get<string>("name");
        Debug.Log(name);
    }

    /// <summary>
    /// �����������x�̎擾
    /// </summary>
    /// <returns>�����������x</returns>
    public float GetFallingSpeed()
    {
        if (meta == null) return 0f;
        float speed = meta.Get<float>("falling_speed");
        return speed;
    }

    public Senario GetSenario(int senarioNumber)
    {
        TomlTable senarioTable = senarios[senarioNumber];
        if (senarioTable == null) return null;

        string name = senarioTable.Get<string>("senario_name");
        SenarioType type = senarioTable.Get<string>("type") == "trainning" ? SenarioType.Training : SenarioType.task;
        float fallingSpeed = GetFallingSpeed();
        int digits = senarioTable.Get<int>("digits");

        // add instrantions
        List<NumberSpawnDelayTimeInstraction> numberSpawnDelayTimeInstractions = new List<NumberSpawnDelayTimeInstraction>();
        for (int i = 0; i <= senarioTable.Get<TomlTableArray>("spawn_delay_times").Count - 1; i++)
        {
            TomlTable table = senarioTable.Get<TomlTableArray>("spawn_delay_times")[i];
            NumberSpawnDelayTimeInstraction instraction = new NumberSpawnDelayTimeInstraction(table.Get<float>("spawn_delay_time"), table.Get<float>("change_at"));
            numberSpawnDelayTimeInstractions.Add(instraction);
        }

        Senario senario = new Senario(name, type, fallingSpeed, numberSpawnDelayTimeInstractions, digits);

        return senario;
    }

    public int GetSenarioLength()
    {
        return senarios.Count;
    }
}
