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
    public string file_path = string.Format("{0}/Senario.toml", Directory.GetCurrentDirectory());

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
        string senarioT = senarioTable.Get<string>("type");
        SenarioType type;
        switch (senarioT)
        {
            case "trainning":
                type = SenarioType.Training;
                break;
            case "measure":
                type = SenarioType.Measure;
                break;
            case "auto":
                type = SenarioType.Auto;
                break;
            case "manual":
                type = SenarioType.Manual;
                break;
            default:
                type = SenarioType.Task;
                break;

        }
        float fallingSpeed = GetFallingSpeed();
        int digits = senarioTable.Get<int>("digits");
        int LifeNumber = senarioTable.Get<int>("life");
        int NumberOfDelete = senarioTable.Get<int>("number_of_delete_on_decrease_life");
        float RangeOfDeleteOnDecreaseLife = senarioTable.Get<float>("range_of_delete_on_decrease_life");

        // add instrantions
        List<NumberSpawnDelayTimeInstraction> numberSpawnDelayTimeInstractions = new List<NumberSpawnDelayTimeInstraction>();
        for (int i = 0; i <= senarioTable.Get<TomlTableArray>("spawn_delay_times").Count - 1; i++)
        {
            TomlTable table = senarioTable.Get<TomlTableArray>("spawn_delay_times")[i];
            NumberSpawnDelayTimeInstraction instraction = new NumberSpawnDelayTimeInstraction(table.Get<float>("spawn_delay_time"), table.Get<float>("change_at"));
            numberSpawnDelayTimeInstractions.Add(instraction);
        }

        Senario senario = new Senario(name, type, fallingSpeed, numberSpawnDelayTimeInstractions, digits, LifeNumber, NumberOfDelete, RangeOfDeleteOnDecreaseLife);

        return senario;
    }

    public int GetSenarioLength()
    {
        return senarios.Count;
    }
}
