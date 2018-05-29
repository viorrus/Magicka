using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;

public class AnimateObject : SkillEvent {
    public Animator activeAnimator;
    public Move3d moveSkill;
    public ProjectilesAttack attackSkill;
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
       
        unit.damageAct += OnDamageHasTaken;
        targetAim = unit.GetComponentInChildren<AimTarget>().transform;
        rootMeshObject = unit.GetComponentInChildren<RootMesh>().transform;
        rootBone = unit.GetComponentInChildren<RootBone>().transform;
        shoot += MakeAttack;
        if (activeAnimator)
        {
            ik = activeAnimator.gameObject.AddComponent<AnimatorIk>();
            ik.Init(targetAim, activeAnimator, rootBone, shoot);
        }
        DOVirtual.DelayedCall(0.1f, () =>
        {
            moveSkill = unit.unitBase.GetSkillByName("Move") as Move3d;
            attackSkill = unit.unitBase.GetSkillByName("Attack") as ProjectilesAttack;
            moveSkill.move += Walk;
            moveSkill.startCrouch += Sitting;
            moveSkill.endCrouch += Sitting;
            moveSkill.startJump += Jump;
            moveSkill.endJump += Land;
            moveSkill.startRotate += Rotate;
            unit.attackAct += Attack;
        });

        if (ik)
        {
            DOVirtual.DelayedCall(Time.deltaTime, () =>
            {
                SetAimFlag((activeAnimator.GetBool("Aiming")));
            }).SetLoops(-1).SetTarget(unit.gameObject).SetUpdate(UpdateType.Late)
                   ;
        }
       
    }

   

    public override void CheckSkill()
    { 

    }

    public override void OnDamageHasTaken(float damage)
    {
        base.OnDamageHasTaken(damage);
    }

    void OnDestroy()
    {
        /*if (unit != null)
        {
            unit.damageAct -= OnDamageHasTaken;
            unit.attackAct -= Attack;
        }
        moveSkill.move -= Walk;
        moveSkill.startCrouch -= Sitting;
        moveSkill.endCrouch -= Sitting;
        moveSkill.startJump -= Jump;
        moveSkill.startRotate -= Rotate;
       
        shoot -= MakeAttack;*/
    }


    public void Walk(float speed)
    {
        activeAnimator.SetBool("Aiming", false);
        activeAnimator.SetFloat("Speed", speed);
    }

    public void Run()
    {
        activeAnimator.SetBool("Aiming", false);
        activeAnimator.SetFloat("Speed", 1f);
    }

    public void Attack()
    {
        Aiming();
            activeAnimator.SetTrigger("Attack");
    }

    public void MakeAttack()
    {
        attackSkill.CmdFire((int)(rootMeshObject.rotation.eulerAngles.y > 100 ? -1: 1));
       // attackSkill.Fire((int)(rootMeshObject.rotation.eulerAngles.y > 100 ? -1 : 1));
    }

    public void Death()
    {
        if (activeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            activeAnimator.Play("Idle", 0);
        else
            activeAnimator.SetTrigger("Death");
    }

    public void Damage()
    {
        if (activeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Death")) return;
      
        activeAnimator.SetInteger("DamageID", 0);
        activeAnimator.SetTrigger("Damage");
    }

    public void Jump()
    {
        activeAnimator.SetBool("Squat", false);
        activeAnimator.SetFloat("Speed", 0f);
        activeAnimator.SetBool("Aiming", false);
        activeAnimator.SetTrigger("Jump");
        activeAnimator.SetBool("IsGround", false);
        DOVirtual.DelayedCall(jumpProcent, moveSkill.AnimDelayJump);
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
      //  activeAnimator.SetTrigger("Rotate"); // Анимация оказалось довольно кривая, что проще стало от нее отказаться. В целом оставил, вдруг найду хорошую
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
