using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlockObject : UnitObject {
   
    public SpriteRenderer spriteR;
    public override void Awake()
    {
    }

    public  void Start()
    {
        unitBase = unitBase.InstantiateMe(transform);
        unitBase.stats = new List<Stat>();
        gameObject.AddComponent<SkillMasterBlock>();
        if (spriteR)
        {
            spriteR.sprite = unitBase.sprite;
        }
    }

    public override void SetState(int i)
    {
        animatorState = i;
        animator.SetInteger("State", animatorState);
    }

    public override void AddSkill(Skill skill)
    {

    }
}
