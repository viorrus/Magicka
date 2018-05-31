using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class Move3d : Move {

    private float xValue;
    private float prevXValue;
    private bool jump;
    public Stat xPos;


    public override void Init()
    {
      
        var temp = unit.GetComponentsInChildren<Stat>().Where(x => x.stringId == xPos.stringId).FirstOrDefault();
        temp.Setup(xPos);
        xPos = temp;  
        unit.unitBase.AddAndGetStat(xPos);
        base.Init();
    }

   

    public override bool SpecificCheckSkill()
    {
        xValue = CrossPlatformInputManager.GetAxis("Horizontal");
        jump = CrossPlatformInputManager.GetButtonDown("Jump");
        if ((prevXValue <= 0  && xValue > 0) || prevXValue >= 0 && xValue < 0) 
        {
            if (startRotate != null)
            {
                startRotate((int)Mathf.Sign(xValue));
            }
        }
        prevXValue = xValue;

        bool result = xValue != 0 || jump && unit.isFree;
        if (result)
        {
            xPos.SetValue(xValue);
        }
        return result;
    }

    public override void UseSkill()
    {
        base.UseSkill();
        if (isGround || isCanControlInAir)
        {
            if (jump && isGround)
            {
                if (startJump != null)
                {
                    startJump();
                }
            }
            else
            {
                rg.velocity = new Vector2(xValue * skillPower, rg.velocity.y);
                if (move != null)
                {
                    move(Mathf.Abs(xValue));
                }
            }
          
        }
    }

    public override void Setup(Skill skill)
    {
        base.Setup(skill);
        xPos = (skill as Move3d).xPos;
    }
}
