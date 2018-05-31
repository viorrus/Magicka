using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : Effect {




    public override void Start()
    {
        base.Start();
        var temp = GetComponentInParent<UnitObject>();
        if (temp)
        {
            var skill = temp.unitBase.GetSkillByName("Attack") as Attack;
            if (skill)
            {
                skill.spawn += AddDamage;

            }
        }
    }

  
    void AddDamage(UnitCollisions unit)
    {
      unit.unitBase.GetStatByName("Damage").AddValue(powerUpValue);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        var temp = GetComponentInParent<UnitObject>();
        if (temp)
        {
            var skill = temp.unitBase.GetSkillByName("Attack") as Attack;
            if (skill)
            {
                skill.spawn -= AddDamage;

            }
        }
    }


}
