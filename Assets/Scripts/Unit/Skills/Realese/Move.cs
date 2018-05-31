using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class Move : SkillWithUse {
    protected Rigidbody rg;
    public bool isCanControlInAir;
    private bool _isGround;
    public bool isGround
    {
        get
        {
            return _isGround;
        }
        set
        {
            if (!_isGround)
            {
                if (value)
                {
                    if (endJump != null)
                    {
                        endJump();
                    }
                   
                }
            }
            _isGround = value;
        }
    }
    private bool _isCrouch;
    public bool isCrouch
    {
        get
        {
            return _isCrouch;
        }
        set
        {
            if(_isCrouch != value)
            {
                if (startCrouch != null)
                {
                    startCrouch();
                }
                _isCrouch = value;
            }
            
        }
    }
    public Transform crouchPoint;
    public float crouchRadius;
    public Transform groundPoint;
    public float groundRadius;
    public LayerMask groundLayers;
    public System.Action startJump;
    public System.Action endJump;
    public System.Action<float> move;
    public System.Action startCrouch;
    public System.Action endCrouch;
    public System.Action<int> startRotate;

    public override void Init()
    {
        rg =  unit.gameObject.GetComponent<Rigidbody>();
        if (!rg)
        {
            rg =  unit.gameObject.AddComponent<Rigidbody>();
        }
        groundPoint = unit.gameObject.GetComponentInChildren<GroundCheck>().transform;
        crouchPoint = unit.gameObject.GetComponentInChildren<CrouchCheck>().transform;

        if (!groundPoint)
        {
            Debug.Log("Нет точки для определения земли");
            return;
        }
        DOVirtual.DelayedCall(Time.fixedDeltaTime, () => isGround = CheckObstacle(groundPoint, groundRadius,groundLayers)).SetLoops(-1).SetUpdate(false).SetTarget(unit.gameObject);
        DOVirtual.DelayedCall(Time.fixedDeltaTime, () => isCrouch = CheckObstacle(crouchPoint, crouchRadius, groundLayers)).SetLoops(-1).SetUpdate(false).SetTarget(unit.gameObject);
    }

   

    public override bool SpecificCheckSkill()
    {
        return true;
    }

    public bool CheckObstacle(Transform point, float radius, LayerMask mask)
    {
        if (point != null)
        {
            Collider[] colliders = Physics.OverlapSphere(point.position, radius, mask);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != unit.gameObject)
                {

                    return true;

                }
            }
        }
        else
        {
            DOTween.Kill(this);
        }
        return false;
    }

    public bool CheckObstacle(Transform point, float radius, LayerMask mask,  out Collider[] colliders )
    {

        colliders = Physics.OverlapSphere(point.position, radius, mask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != unit.gameObject)
            {

                return true;

            }
        }

        return false;
    }


    public override void UseSkill()
    {
        base.UseSkill();
    }

    public void AnimDelayJump()
    {
        rg.AddForce(new Vector3(0, skillPower, 0), ForceMode.Impulse);
    }

    public override void Setup(Skill skill)
    {
        base.Setup(skill);
        groundLayers = (skill as Move).groundLayers;
        crouchRadius = (skill as Move).crouchRadius;
        groundRadius = (skill as Move).groundRadius;
        isCanControlInAir = (skill as Move).isCanControlInAir;
    }
}
