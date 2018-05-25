using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (madeAttack != null)
        {
            madeAttack();
        }
        if (!fxParent)
        {
            fxParent = new GameObject(unit.unitBase.unitName + "_Fx");
        }
      
    }

    public void MakeFire(int direction)
    {
        firePositions.ForEach(x =>
        {
            tempProjectile = Instantiate(projectile, x.transform.position, x.transform.rotation, fxParent.transform);
            tempProjectile.GetComponent<Rigidbody>().AddForce(direction*Vector3.right * speed, ForceMode.Impulse);
        }
        );
    }
    
}
