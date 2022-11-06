using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.Scripts.LoadSenarios;

public class LoadSenarioTest
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Debug.Log("SetUp");
    }

    [Test]
    public void LoadSenario_LoadSenario()
    {
        SenarioTomlRepo senarioTomlRepo = new SenarioTomlRepo();
        Assert.IsNotNull(senarioTomlRepo);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void LoadSenarioTest_LoadFallingSpeed()
    {
        SenarioTomlRepo senarioRepo = new SenarioTomlRepo();
        float fallingSpeed = senarioRepo.GetFallingSpeed();

        Assert.That(fallingSpeed != 0f);

    }

    [Test]
    public void LoadSenarioTest_GetSenario0()
    {
        SenarioTomlRepo senarioTomlRepo = new SenarioTomlRepo();
        Senario senario = senarioTomlRepo.GetSenario(0);

        Assert.IsNotNull(senario);
        Assert.That(senario.NumberSpawnDelayTimeInstractions.Count != 0);
    }

    [Test]
    public void LoadSenarioTest_LastDeltaTime()
    {
        SenarioTomlRepo senarioTomlRepo = new SenarioTomlRepo();
        Senario senario = senarioTomlRepo.GetSenario(0);

        float delta = senario.GetSpawnDelayTimeByTime(1000000f);
        Assert.That(delta == -1000f);
    }
}
