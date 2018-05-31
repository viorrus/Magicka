using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : Effect {

 
   
    public override void Start()
    {
        base.Start();
        (GetComponentInParent<UnitPlayerObject>().unitBase.GetSkillByName("Health") as HealthChanger).stat.SetValue(powerUpValue);
    }
}
