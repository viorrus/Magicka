using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Freeze : SkillEvent {

    public GameObject effect;
    Collider tempCollider;

    public override void Init()
    {
        unit.triggerIN += TriggerEnter;
    }

    private void TriggerEnter(Collider other, UnitObject source)
    {
        tempCollider = other;
        var temp = other.GetComponent<UnitObject>();

        if (source != null && source!=temp)
        {
            (source as UnitCollisions).DeActivate();
        }
        if (temp && temp.GetComponentInChildren<Freeze>() == null)
        {
            CmdSpawn();
        }
    }

    public override void Setup(Skill skill)
    {
        base.Setup(skill);
        effect = (skill as Freeze).effect;
    }

    [Command]
    void CmdSpawn()
    {
    
        var tempSpawn = effect.InstantiateMe(tempCollider.transform.position, Quaternion.identity, tempCollider.transform);
        tempSpawn.GetComponent<NetworkSpawner>().parentNetId = tempCollider.GetComponent<NetworkIdentity>().netId;
        NetworkServer.Spawn(tempSpawn);
    }

    void OnDestroy()
    {
        if (unit != null && unit.triggerIN != null)
        {
            unit.triggerIN -= TriggerEnter;
        }
       
    }
}
