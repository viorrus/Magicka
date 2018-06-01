using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class ProjectilesAttack : Attack {



    public override void Init()
    {
        base.Init();
        firePositions = new List<MuzzleFx>(unit.transform.GetComponentsInChildren<MuzzleFx>());
    }

    public override bool SpecificCheckSkill()
    {
        return CrossPlatformInputManager.GetButtonDown(button) && unit.isFree;
    }

    public override void UseSkill()
    {
        base.UseSkill();
        CmdPrepareAttack();
    }

    public override void Setup(Skill skill)
    {
        base.Setup(skill);
        button = (skill as ProjectilesAttack).button;
    }

}
