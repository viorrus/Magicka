using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Spawner : SkillWithUse {
   

    public bool isFree;

    public GameObject[] spawnObjects;

    public override void Init()
    {
        isFree = true;
    }

    public override bool SpecificCheckSkill()
    {
        return isFree;
    }

    public override void UseSkill()
    {
        base.UseSkill();
        isFree = false;
        CmdSpawn();
    }

    public override void Setup(Skill skill)
    {
        base.Setup(skill);
        spawnObjects = (skill as Spawner).spawnObjects;
    }

    [Command]
   public virtual void CmdSpawn()
    {
        var spawnObj = spawnObjects[UnityEngine.Random.Range(0, spawnObjects.Length)].InstantiateMe(unit.transform.position, unit.transform.rotation, unit.transform);
        spawnObj.GetComponent<NetworkSpawner>().parentNetId = unit.GetComponent<NetworkIdentity>().netId;
        NetworkServer.Spawn(spawnObj);
    }

    public void GetPowerUp()
    {
        isFree = true;
    }

}
