using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEffect : Effect {

    float tempValue;
    Skill skill;
   
    public override void Start()
    {
        base.Start();

        skill = (GetComponentInParent<UnitObject>().unitBase.GetSkillByName("Move"));
        if(skill != null)
        {
            skill.unit.isFree = false;
            tempValue = skill.skillPower;
            skill.skillPower = powerUpValue;
        }
      
    }

  
   

    public override void OnDestroy()
    {
        base.OnDestroy();
        if (skill != null)
        {
            skill.unit.isFree = true;
            skill.skillPower = tempValue;
        }
    }


}
