using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenBullet : Effect {

    public GameObject projectiles;
    GameObject tempProjetiles;
    Attack skill;
   
    public override void Start()
    {
        base.Start();
        var temp = GetComponentInParent<UnitObject>();
        if (temp)
        {

            skill = temp.unitBase.GetSkillByName("Attack") as Attack;
            if (skill)
            {
                tempProjetiles = skill.projectile;
                skill.projectile = projectiles;
            }
        }
       
    }

  
   

    public override void OnDestroy()
    {
        base.OnDestroy();
        if (skill != null)
        {
            skill.projectile = tempProjetiles;
        }
    }


}
