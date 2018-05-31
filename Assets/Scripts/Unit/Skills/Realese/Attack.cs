﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class Attack : SkillWithUse {
    public string button;
    public GameObject projectile;
    private GameObject tempProjectile;
    public float speed;
    public List<MuzzleFx> firePositions;
    protected GameObject fxParent;
    public System.Action madeAttack;
    public System.Action<UnitCollisions> spawn;

    public override void Init()
    {
        base.Init();
        firePositions = new List<MuzzleFx>(unit.transform.GetComponentsInChildren<MuzzleFx>());
    }

    public void CmdPrepareAttack()
    {
            if (!fxParent)
            {
                fxParent = new GameObject(unit.unitBase.unitName + "_Fx");
            }
            if (unit.attackAct != null)
            {
                unit.attackAct();
            } 
    }

  
    [Command]
    public void CmdFire(int direction)
    {
        SoundManager.Instance.PlaySound("attack");
        unit.isFree = true;
        firePositions.ForEach(x =>
        {
            tempProjectile = Instantiate(projectile, x.transform.position, x.transform.rotation);
            tempProjectile.GetComponent<Rigidbody>().velocity = direction*Vector3.right * speed;
            var tempUnit = tempProjectile.GetComponent<UnitCollisions>();
            tempUnit.ownerID = unit.ID;
            if(spawn != null)
            {
                spawn(tempUnit);
            }
            NetworkServer.Spawn(tempProjectile);
            Destroy(tempProjectile, 3);
        }
        );
    }

    public override void Setup(Skill skill)
    {
        base.Setup(skill);
        speed = (skill as Attack).speed;
        projectile = (skill as Attack).projectile;
    }

}