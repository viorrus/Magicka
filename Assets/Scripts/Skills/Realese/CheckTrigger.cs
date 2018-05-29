using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrigger : SkillEvent {

    public override void Init()
    {
        unit.triggerIN += TriggerEnter;
    }

    private void TriggerEnter(Collider other)
    {
        var temp = other.GetComponent<UnitObject>();
        if (temp)
        {
            temp.GetDamage(unit.unitBase.GetStatByName("Damage").GetValue());
        }
    }

    void OnDestroy()
    {
        unit.triggerIN -= TriggerEnter;
    }
}
