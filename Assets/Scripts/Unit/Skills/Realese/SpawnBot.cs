using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnBot : Spawner {
   

    public override void Init()
    {
        isFree = true;
    }

    public override bool SpecificCheckSkill()
    {
        return isFree && GameManager.Instance.CheckSpawn();
    }

    public override void Setup(Skill skill)
    {
        base.Setup(skill);
    }

    [Command]
    public override void CmdSpawn()
    {
        var spawnObj = spawnObjects[UnityEngine.Random.Range(0, spawnObjects.Length)].InstantiateMe(unit.transform.position, unit.transform.rotation, unit.transform);
        spawnObj.GetComponent<NetworkSpawner>().parentNetId = unit.GetComponent<NetworkIdentity>().netId;
        NetworkServer.Spawn(spawnObj);
        GameManager.Instance.botCount++;
    }

}
