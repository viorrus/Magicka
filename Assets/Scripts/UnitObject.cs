using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class UnitObject : NetworkBehaviour {
    public System.Action deathAct;
    public System.Action<Collider> triggerIN;
    public System.Action<float> damageAct;
    public System.Action attackAct;
    public Unit unitBase;
    public bool isFree;
    public Animator animator;
    public int animatorState;
    public NavMeshAgent agent;
    public int ID;

    public  virtual  void Start()
    {
        unitBase = unitBase.InstantiateMe(transform);

        List<Stat> temp = new List<Stat>(GetComponentsInChildren<Stat>());
        temp.ForEach(x =>
        {
            if (unitBase.GetStatByName(x.stringId) != null)
            { x.Setup(unitBase.GetStatByName(x.stringId)); }
        }  );
        unitBase.stats = new List<Stat>(temp);
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

    public virtual void GetDamage(float damage)
    {

    }

    private void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
