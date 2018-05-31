using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class AttackAI : Attack {

    public Move moveSkill;
    bool isAttack;


    public override void Init()
    {
        base.Init();
        firePositions = new List<MuzzleFx>(unit.transform.GetComponentsInChildren<MuzzleFx>());
        DOVirtual.DelayedCall(0.1f, () =>
        {
            moveSkill = unit.unitBase.GetSkillByName("Move") as MoveAI;
            (moveSkill as MoveAI).destination += SetAttack;
        });
    }

    void SetAttack()
    {
        isAttack = true;
    }

    public override bool SpecificCheckSkill()
    {
        return isAttack;
    }

    public override void UseSkill()
    {
        base.UseSkill();
        isAttack = false;
        unit.isFree = true;
        if(unit.attackAct != null)
        {
            unit.attackAct();
        }
    }
}
