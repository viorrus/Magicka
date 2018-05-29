using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    }


    public override void UseSkill()
    {
    }

  

    public override void Setup(Skill skill)
    {
     
    }



}
