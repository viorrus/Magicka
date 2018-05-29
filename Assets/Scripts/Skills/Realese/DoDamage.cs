using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : SkillEvent {

    public override void Init()
    {
        unit.triggerIN += TriggerEnter;
    }

    private void TriggerEnter(Collider other)
    {
        var temp = other.GetComponent<UnitObject>();
        if (temp)
        {
            temp.GetDamage(unit.unitBase.GetStatByName("Damage").GetValue(), (unit as UnitCollisions).ownerID);
        }
    }

    void OnDestroy()
    {
        unit.triggerIN -= TriggerEnter;
    }
}
