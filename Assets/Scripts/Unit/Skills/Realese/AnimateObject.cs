using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;

public class AnimateObject : SkillEvent {
    public Animator activeAnimator;
    NetworkAnimator netAnim;
    public Move moveSkill;
    public Attack attackSkill;
    public float jumpProcent;
    private Transform targetAim;
    private Transform rootBone;
    private Transform rootMeshObject;
    public Vector3 offset;
    private AnimatorIk ik;
    private System.Action shoot;




    public override void Init()
    {
      
        activeAnimator = unit.GetComponentInChildren<Animator>();
        netAnim = unit.GetComponentInChildren<NetworkAnimator>();

        unit.damageAct += OnDamageHasTaken;
        var aim = unit.GetComponentInChildren<AimTarget>();
        targetAim = aim ? aim.transform:null;
        rootMeshObject = unit.GetComponentInChildren<RootMesh>().transform;
        var bone = unit.GetComponentInChildren<RootBone>();
        rootBone = bone? bone.transform:null;
        shoot += MakeAttack;
        if (activeAnimator)
        {
            ik = activeAnimator.gameObject.AddComponent<AnimatorIk>();
            ik.Init(targetAim, activeAnimator, rootBone, shoot);
        }
        DOVirtual.DelayedCall(0.1f, () =>
        {
            moveSkill = unit.unitBase.GetSkillByName("Move") as Move;
            attackSkill = unit.unitBase.GetSkillByName("Attack") as Attack;
            moveSkill.move += Walk;
            moveSkill.startCrouch += Sitting;
            moveSkill.endCrouch += Sitting;
            moveSkill.startJump += JumpStart;
            moveSkill.endJump += Land;
            moveSkill.startRotate += Rotate;
            moveSkill.groundChange += SetGround;
            unit.attackAct += Attack;
        });

       
    }

   

    public override void CheckSkill()
    { 

    }

    public override void OnDamageHasTaken(float damage, NetworkInstanceId id)
    {
        base.OnDamageHasTaken(damage, id);
    }

    void OnDestroy()
    {
       
    }


    public void Walk(float speed)
    {
      
        activeAnimator.SetFloat("Speed", speed);
    }

    public void Run()
    {
       
        activeAnimator.SetFloat("Speed", 1f);
    }

    public void Attack()
    {
        Aiming();    
    }

    public void MakeAttack()
    {
        attackSkill.CmdFire((int)(rootMeshObject.rotation.eulerAngles.y > 100 ? -1: 1));
        activeAnimator.SetBool("Aiming", false);
    }

    public void Death()
    {
        if (activeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            activeAnimator.Play("Idle", 0);
        else
            activeAnimator.SetTrigger("Death");
    }

  

    public void JumpStart()
    {
        activeAnimator.SetBool("Squat", false);
        activeAnimator.SetFloat("Speed", 0f);
        activeAnimator.SetTrigger("Jump");
        activeAnimator.SetBool("IsGround", false);
        DOVirtual.DelayedCall(jumpProcent, moveSkill.AnimDelayJump);
    }

    public void SetGround(bool isGriund)
    {
        activeAnimator.SetBool("IsGround", isGriund);
    }

    public void Land()
    {
        activeAnimator.SetBool("IsGround", true);
    }

    public void Aiming()
    {
        activeAnimator.SetBool("Aiming", true);
    }

    public void Rotate(int direction)
    {
        activeAnimator.SetInteger("Direction", direction);
         Sequence sq = DOTween.Sequence();
          sq.SetDelay(0.1f)
              .Append(rootMeshObject.DOLocalRotate(new Vector3(0, 90 * direction, 0), 0.8469388f));
    }

    public void Sitting()
    {
        activeAnimator.SetBool("Squat", !activeAnimator.GetBool("Squat"));
        activeAnimator.SetBool("Aiming", false);
    }

    private void Aim()
    {
        rootBone.LookAt(targetAim);
        rootBone.rotation *= Quaternion.Euler(offset);
    }

    private void SetAimFlag(bool flag)
    {
        ik.SetFlag(flag);
    }
}
