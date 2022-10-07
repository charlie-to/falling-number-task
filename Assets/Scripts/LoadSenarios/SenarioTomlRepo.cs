using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nett;
using System.IO;
using Unity.VisualScripting;
using System;
using Assets.Scripts.LoadSenarios;
using NPOI.SS.Formula.Functions;

public class SenarioTomlRepo
{
    public string file_name;

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
        // read input file
        //string input_file_path = Directory.GetCurrentDirectory() + directry_name + file_name;
        var toml_file = string.Format("{0}/../Senario.toml", Application.dataPath);

        try
        {
            TomlTable root = Toml.ReadFile(toml_file);
            meta = root.Get<TomlTable>("meta");
            senarios = root.Get<TomlTableArray>("Senarios");
        }
        catch
        {
            throw new ArgumentException("toml file is not found!");
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

        // add instrantions
        List<NumberSpawnDelayTimeInstraction> numberSpawnDelayTimeInstractions = new List<NumberSpawnDelayTimeInstraction>();
        for (int i = 0; i <= senarioTable.Get<TomlTableArray>("spawn_delay_times").Count -1 ; i++)
        {
            TomlTable table = senarioTable.Get<TomlTableArray>("spawn_delay_times")[i];
            NumberSpawnDelayTimeInstraction instraction = new NumberSpawnDelayTimeInstraction(table.Get<float>("spawn_delay_time"), table.Get<float>("change_at"));
            numberSpawnDelayTimeInstractions.Add(instraction);
        }

        Senario senario = new Senario(name, type , fallingSpeed, numberSpawnDelayTimeInstractions);

        return senario;
    }
}