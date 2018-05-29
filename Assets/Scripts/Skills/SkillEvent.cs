using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public virtual void OnDamageHasTaken(float damage,string id)
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
        manacost = skill.manacost;
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
