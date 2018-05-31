using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SkillEvent : Skill {

    public override void Init()
    {
      //Подпишись
    }

   

    public override void CheckSkill()
    { 
    }

    public virtual void OnDamageDealt()
    {
        //Сотвори чудо. Пример
    }

    public virtual void OnDamageHasTaken(float damage, NetworkInstanceId id)
    {
        //Сотвори чудо. Пример
    }

    public override void UseSkill()
    {
    }

    void OnDestroy()
    {
        //Отпишись
    }

    public override void Setup(Skill skill)
    {
        castTime = skill.castTime;
        cdTime = skill.cdTime;
        durationTime = skill.durationTime;
        castRange = skill.castRange;
        skillPower = skill.skillPower;//использовать по умолчанию
        usePriority = skill.usePriority;
        isAlwaysUse = skill.isAlwaysUse;
        skillImage = skill.skillImage;
    }



}
