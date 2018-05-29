using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : SkillEvent {


    public override void Init()
    {
        unit.triggerIN += TriggerEnter;
    }

    public virtual void TriggerEnter(Collider other)
    {
        GetComponentInParent<SpawnPowerUp>().GetPowerUp();
       
    }

    void OnDestroy()
    {
        unit.triggerIN -= TriggerEnter;
    }
}
