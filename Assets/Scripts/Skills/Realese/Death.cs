using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Death : SkillEvent {

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
        var killerunit = FindObjectsOfType<UnitPlayerObject>().Where(x => x.ID == (unit as UnitPlayerObject).idLastHit).FirstOrDefault();
        if(killerunit != null)
        {
            killerunit.fragCount++;
        }
        if (isServer && isLocalPlayer)
        {
            NetworkManager.singleton.StopHost();
        }
        else if(isLocalPlayer)
        {
            Network.Disconnect(2);
            unit.GetComponent<NetworkIdentity>().connectionToServer.Disconnect();
            unit.gameObject.SetActive(false);
        }
       
       
    }


    public override void UseSkill()
    {
    }

  

    public override void Setup(Skill skill)
    {
        base.Setup(skill);
        stat = (skill as Death).stat;
    }



}
