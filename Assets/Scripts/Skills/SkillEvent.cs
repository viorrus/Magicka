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

    public virtual void OnDamageDealt(float damageF, float damageM, Transform source)
    {
        //Сотвори чудо. Пример
    }

    public virtual void OnDamageHasTaken(float damageF, float damageM)
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



}
