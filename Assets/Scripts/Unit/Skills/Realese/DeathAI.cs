using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class DeathAI : SkillEvent {

    public Stat stat;

    public override void Init()
    {
        var temp = unit.GetComponentsInChildren<Stat>().Where(x => x.stringId == stat.stringId).FirstOrDefault();
        temp =  unit.unitBase.AddAndGetStat(temp);
        temp.valueisMin += OnDeath;
        temp.Setup(stat);
        stat = temp;
    }

    void OnDeath()
    {
        RpcDeath();
    }

    [ClientRpc]
    void RpcDeath()
    {
        SetDeath();
    }

    void SetDeath()
    {

        GameManager.Instance.botCount--;
        var killerunit = NetworkServer.FindLocalObject(unit.idLastHit);
        if(killerunit != null)
        {
            killerunit.GetComponent<UnitPlayerObject>().fragCount++;
        }

        var spawn = GetComponentInParent<Spawner>();
        if (spawn)
        {
            spawn.GetPowerUp();
        }
        Destroy(gameObject);  

        
    }


    public override void UseSkill()
    {
    }

  

    public override void Setup(Skill skill)
    {
        base.Setup(skill);
        stat = (skill as DeathAI).stat;
    }



}
