using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AnimateObject : SkillEvent {
    public Animator activeAnimator;
    public Move3d moveSkill;
    public float jumpProcent;



    public override void Init()
    {
        activeAnimator = unit.GetComponentInChildren<Animator>();
        unit.damageAct += OnDamageHasTaken;
        DOVirtual.DelayedCall(0.1f, () =>
        {
            moveSkill = unit.unitBase.GetSkillByName("Move") as Move3d;
            moveSkill.move += Walk;
            moveSkill.startCrouch += Sitting;
            moveSkill.endCrouch += Sitting;
            moveSkill.startJump += Jump;
            moveSkill.startRotate += Rotate;
        });
      
    }

   

    public override void CheckSkill()
    { 

    }

    public override void OnDamageHasTaken()
    {
        base.OnDamageHasTaken();
    }

    void OnDestroy()
    {
        unit.damageAct -= OnDamageHasTaken;
        moveSkill.move -= Walk;
        moveSkill.startCrouch -= Sitting;
        moveSkill.endCrouch -= Sitting;
        moveSkill.startJump -= Jump;
        moveSkill.startRotate -= Rotate;
    }

    public void Stay()
    {
        activeAnimator.SetBool("Aiming", false);
        activeAnimator.SetFloat("Speed", 0f);
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
        DOVirtual.DelayedCall(jumpProcent, moveSkill.AnimDelayJump);
    }

    public void Aiming()
    {
        activeAnimator.SetBool("Squat", false);
        activeAnimator.SetFloat("Speed", 0f);
        activeAnimator.SetBool("Aiming", true);
    }

    public void Rotate(int direction)
    {
        activeAnimator.SetInteger("Direction", direction);
      //  activeAnimator.SetTrigger("Rotate");
         Sequence sq = DOTween.Sequence();
          sq.SetDelay(0.1f)
              .Append(activeAnimator.transform.DOLocalRotate(new Vector3(0, 90 * direction, 0), 0.8469388f));


    }

    public void Sitting()
    {
        activeAnimator.SetBool("Squat", !activeAnimator.GetBool("Squat"));
        activeAnimator.SetBool("Aiming", false);
    }


}
