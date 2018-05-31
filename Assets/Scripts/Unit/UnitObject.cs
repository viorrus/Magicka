using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class UnitObject : NetworkBehaviour {
    public System.Action deathAct;
    public System.Action<Collider, UnitObject> triggerIN;
    public System.Action<float, NetworkInstanceId> damageAct;
    public System.Action attackAct;
    public Unit unitBase;
    public bool isFree;
    public Animator animator;
    public int animatorState;
    public NavMeshAgent agent;
    [SyncVar]
    public NetworkInstanceId ID;
    [SyncVar]
    public NetworkInstanceId parentNetId;
    [SyncVar]
    public NetworkInstanceId idLastHit;

    public  virtual  void Awake()
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

    public virtual void GetDamage(float damage, NetworkInstanceId id)
    {
        GetDamage(id, damage);

    }

    void GetDamage(NetworkInstanceId id, float damage)
    {
        if (damageAct != null)
        {
            damageAct(damage, id);
        }
    }

    private void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
