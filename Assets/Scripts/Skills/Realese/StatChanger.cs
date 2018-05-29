using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatChanger : SkillWithUse {
    public Stat stat;

    public override void Init()
    {
        stat = stat.InstantiateMe(unit.transform);
        stat = unit.unitBase.AddAndGetStat(stat);
    }


    public override void UseSkill()
    {
        if(timer >= Time.time + cdTime)
        {
            timer = Time.time;
            stat.AddValue(skillPower);
        }
    }

    public override void Setup(Skill skill)
    {
        base.Setup(skill);
        stat = (skill as StatChanger).stat;
    }

}
