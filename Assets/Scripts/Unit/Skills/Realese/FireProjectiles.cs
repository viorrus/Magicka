using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class FireProjectiles : SkillWithUse {
    public string button;
    public Stat xPos;
    public Stat yPos;
    private float xValue;
    private float yValue;
    public GameObject projectile;
    private GameObject tempProjectile;
    public float speed;

    public override void Init()
    {
        base.Init();

        xPos = xPos.InstantiateMe(unit.transform);
        xPos = unit.unitBase.AddAndGetStat(xPos);
        yPos = yPos.InstantiateMe(unit.transform);
        yPos = unit.unitBase.AddAndGetStat(yPos);
    }

    public override bool SpecificCheckSkill()
    {

        return CrossPlatformInputManager.GetButtonDown(button);
    }

    public override void UseSkill()
    {
        base.UseSkill();

        xValue = xPos.GetValue();
        yValue = yPos.GetValue();
        if(xValue != 0)
        {
            xValue = xValue > 0 ? 1 : -1;
        }
        if (yValue != 0)
        {
            yValue = yValue > 0 ? 1 : -1;
        }
        tempProjectile = Instantiate(projectile, unit.transform.position + new Vector3(xValue, yValue, 0), unit.transform.rotation);
        tempProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(xValue * speed, yValue * speed), ForceMode2D.Impulse);
    }
}
