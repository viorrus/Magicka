using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceEffect : Effect {

 
   
    public override void Start()
    {
        base.Start();
        var temp = GetComponentInParent<UnitObject>();
        if (temp)
        {
            var skill = temp.unitBase.GetSkillByName("Health") as HealthChanger;
            if (skill)
            {
                skill.armor = powerUpValue;

            }
        }
    }

  
   

    public override void OnDestroy()
    {
        base.OnDestroy();
        var temp = GetComponentInParent<UnitObject>();
        if (temp)
        {
            var skill = temp.unitBase.GetSkillByName("Health") as HealthChanger;
            if (skill)
            {
                skill.armor = 0;

            }
        }
       
    }


}
