using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenarioRepository
{
    [SerializeField] private Entity_test1 test1;

    private List<Entity_test1> entity_Tests = new List<Entity_test1> ();

    public SenarioRepository()
    {
        test1 = Resources.Load<Entity_test1> ("TaskSenario");

        entity_Tests.Add(test1);
    }

    /// <summary>
    /// Žw’èID‚ÌMonsterParam‚ð•Ô‹p
    /// </summary>
    public Entity_test1.Param GetSenarioParam(int senarioId, int eventId)
    {
        var param = entity_Tests[senarioId].sheets[0].list.Find(e => e.id == eventId);

        if (param == null) {Debug.LogError("not found event!! id: " + eventId);}
    
        return param;
    }

    public int GetSenarioListLength() { return entity_Tests.Count; }

}
