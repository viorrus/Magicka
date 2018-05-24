using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitObject : MonoBehaviour {
    public System.Action deathAct;
    public System.Action damageAct;


    public Unit unitBase;
    public bool isFree;
    public Animator animator;
    public int animatorState;
    public NavMeshAgent agent;

    public  virtual  void Start()
    {
        unitBase = unitBase.InstantiateMe(transform);
        unitBase.stats = new List<Stat>();
        gameObject.AddComponent<SkillMasterUnit>();
    }

    public virtual void SetState(int i)
    {
        animatorState = i;
        animator.SetInteger("State", animatorState);
    }

    public virtual void AddSkill(Skill skill)
    {

    }

    public void GetDamage(int damage)
    {
        if (damageAct!=null)
        {
            damageAct();
        }
       
    }
}
