using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class ProjectilesAttack : SkillWithUse {
    public string button;
    public GameObject projectile;
    private GameObject tempProjectile;
    public float speed;
    public List<MuzzleFx> firePositions;
    private GameObject fxParent;
    public System.Action madeAttack;

    public override void Init()
    {
        base.Init();
        firePositions = new List<MuzzleFx>(unit.transform.GetComponentsInChildren<MuzzleFx>());
    }

    public override bool SpecificCheckSkill()
    {

        return CrossPlatformInputManager.GetButtonDown(button);
    }

    public override void UseSkill()
    {
        base.UseSkill();

        CmdPrepareAttack();
        if (!fxParent)
        {
            fxParent = new GameObject(unit.unitBase.unitName + "_Fx");
        }
        if (unit.attackAct != null)
        {
            unit.attackAct();
        }
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
    
        firePositions.ForEach(x =>
        {
            tempProjectile = Instantiate(projectile, x.transform.position, x.transform.rotation);
            tempProjectile.GetComponent<Rigidbody>().velocity = direction*Vector3.right * speed;
            NetworkServer.Spawn(tempProjectile);
            Destroy(tempProjectile, 3);
        }
        );
    }

    public void Fire(int direction)
    {
        if (!isServer)
        {
            firePositions.ForEach(x =>
            {
                tempProjectile = Instantiate(projectile, x.transform.position, x.transform.rotation, fxParent.transform);
                tempProjectile.GetComponent<Rigidbody>().AddForce(direction * Vector3.right * speed, ForceMode.Impulse);
                Destroy(tempProjectile, 3);
            }
            );
        }
    }

    public override void Setup(Skill skill)
    {
        base.Setup(skill);
        speed = (skill as ProjectilesAttack).speed;
        projectile = (skill as ProjectilesAttack).projectile;
        button = (skill as ProjectilesAttack).button;
    }

}
