using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnPowerUp : SkillWithUse {
   

    public bool isFree;

    public GameObject[] powerUps;

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
        powerUps = (skill as SpawnPowerUp).powerUps;
    }

    [Command]
    void CmdSpawn()
    {
        NetworkServer.Spawn(powerUps[UnityEngine.Random.Range(0, powerUps.Length)].InstantiateMe(unit.transform.position, unit.transform.rotation, unit.transform));
    }

    public void GetPowerUp()
    {
        isFree = true;
    }

}
