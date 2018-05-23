using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Move : SkillWithUse {

    private float xValue;
    private float yValue;
    public Stat xPos;
    public Stat yPos;
    private Rigidbody2D rigidbody;

    public override void Init()
    {
        rigidbody =  unit.gameObject.GetComponent<Rigidbody2D>();
        xPos = xPos.InstantiateMe(unit.transform);
        xPos = unit.unitBase.AddAndGetStat(xPos);
        yPos = yPos.InstantiateMe(unit.transform);
        yPos = unit.unitBase.AddAndGetStat(yPos);
    }

    public override bool SpecificCheckSkill()
    {
        xValue = CrossPlatformInputManager.GetAxis("Horizontal");
        yValue = CrossPlatformInputManager.GetAxis("Vertical");

        bool result = xValue != 0 || yValue != 0;
        if (result)
        {
            xPos.SetValue(xValue);
            yPos.SetValue(yValue);
        }

        return result;
    }

    public override void UseSkill()
    {
        base.UseSkill();
        rigidbody.velocity = new Vector2(xValue * skillPower, yValue * skillPower);
    }


}
