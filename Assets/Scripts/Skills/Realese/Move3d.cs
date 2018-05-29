using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class Move3d : SkillWithUse {

    private float xValue;
    private float prevXValue;
    private bool jump;
    public Stat xPos;
    private Rigidbody rigidbody;
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
        rigidbody =  unit.gameObject.GetComponent<Rigidbody>();
        if (!rigidbody)
        {
            rigidbody =  unit.gameObject.AddComponent<Rigidbody>();
        }
        var temp = unit.GetComponentsInChildren<Stat>().Where(x => x.stringId == xPos.stringId).FirstOrDefault();
        temp.Setup(xPos);
        xPos = temp;  
        unit.unitBase.AddAndGetStat(xPos);
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

        xValue = CrossPlatformInputManager.GetAxis("Horizontal");
        jump = CrossPlatformInputManager.GetButtonDown("Jump");
        if ((prevXValue <= 0  && xValue > 0) || prevXValue >= 0 && xValue < 0) 
        {
            if (startRotate != null)
            {
                startRotate((int)Mathf.Sign(xValue));
            }
        }
        prevXValue = xValue;

        bool result = xValue != 0 || jump;
        if (result)
        {
            xPos.SetValue(xValue);
        }
        return result;
    }

    public bool CheckObstacle(Transform point, float radius, LayerMask mask)
    {
        
        Collider[] colliders = Physics.OverlapSphere(point.position, radius, mask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != unit.gameObject)
            {

                return true;
               
            }  
        }

        return false;
    }

    void CheckState(bool newValue)
    {
        if (isCrouch != newValue)
        {
            if (isCrouch)
            {
                startCrouch();
            }
            else
            {
                endCrouch();
            }
        }
    }



    public override void UseSkill()
    {
        base.UseSkill();
        if (isGround || isCanControlInAir)
        {
            if (jump && isGround    )
            {
                if (startJump != null)
                {
                    startJump();
                }
            }
            else
            {
                rigidbody.velocity = new Vector2(xValue * skillPower, rigidbody.velocity.y);
                if (move != null)
                {
                    move(Mathf.Abs(xValue));
                }
            }
          
        }
    }

    public void AnimDelayJump()
    {
        rigidbody.AddForce(new Vector3(0, skillPower, 0), ForceMode.Impulse);
    }

    public override void Setup(Skill skill)
    {
        base.Setup(skill);

        xPos = (skill as Move3d).xPos;
        groundLayers = (skill as Move3d).groundLayers;
        crouchRadius = (skill as Move3d).crouchRadius;
        groundRadius = (skill as Move3d).groundRadius;
        isCanControlInAir = (skill as Move3d).isCanControlInAir;
    }
}
