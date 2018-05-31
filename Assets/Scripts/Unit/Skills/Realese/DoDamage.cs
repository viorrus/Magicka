using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : SkillEvent {
    public bool isDestoyOnTrigger;

    public override void Init()
    {
        unit.triggerIN += TriggerEnter;
    }

    private void TriggerEnter(Collider other, UnitObject source)
    {
        var temp = other.GetComponent<UnitObject>();
        if (isDestoyOnTrigger)
        {
            (source as UnitCollisions).DeActivate();
        }
        if (temp)
        {
            temp.GetDamage(unit.unitBase.GetStatByName("Damage").GetValue(), (unit as UnitCollisions).ownerID);
        }  
    }

    public override void Setup(Skill skill)
    {
        base.Setup(skill);
        isDestoyOnTrigger = (skill as DoDamage).isDestoyOnTrigger;
    }

    void OnDestroy()
    {
        unit.triggerIN -= TriggerEnter;
    }
}
