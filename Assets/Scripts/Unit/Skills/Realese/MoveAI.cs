using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class MoveAI : Move {

    private int currentDirection = 1;
    private int prevDirectioin = 1;
    public string waterId;
    bool fly;
    List<UnitPlayerObject> players;
    public float aggroRadius;
    public LayerMask playersLayer;
    Transform target;
    public float stopDistance;
    public System.Action destination;

    public override void Init()
    {
        base.Init();
    }

    public override bool SpecificCheckSkill()
    {
        bool result = unit.isFree;
        if (result)
        {
            CheckEnemy();
            CheckDirection();
            CheckWater();
            if(currentDirection != prevDirectioin)
            {
                if (startRotate != null)
                {
                    startRotate(prevDirectioin);
                }
                result &= false;
            }
        }
        
        return result ;
    }

    void CheckDirection()
    {
        Ray ray = new Ray(unit.transform.position + unit.transform.up *0.2f, unit.transform.right * prevDirectioin);
        if(!Physics.SphereCast(ray, 0.1f, 0.2f, groundLayers))
        {
            currentDirection = prevDirectioin; 
        }
        else
        {
            prevDirectioin *= -1;
        }
       
    }

    void CheckWater()
    {
        Ray ray = new Ray(unit.transform.position + unit.transform.right * currentDirection/2, (-1) * unit.transform.up);
        RaycastHit hit;
        if (!Physics.Raycast(ray,out hit,0.2f, groundLayers))
        {
          fly =  false;
        }
        else
        {
            fly = true;
        }
      
    }

    void CheckEnemy()
    {
        Collider[] colliders;
        if(CheckObstacle(unit.transform, aggroRadius, playersLayer, out colliders))
        {
            target = colliders.OrderBy(x => (x.transform.position - unit.transform.position).magnitude).FirstOrDefault().transform;
        }
        else
        {
            target = null;
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();

        if (target)
        {
            if (target.position.x < unit.transform.position.x && currentDirection > 0)
            {
                currentDirection *= -1;
                prevDirectioin = currentDirection;
                if (startRotate != null)
                {
                    startRotate(currentDirection);
                }
            }
            else if (target.position.x > unit.transform.position.x && currentDirection < 0)
            {
                currentDirection *= -1;
                prevDirectioin = currentDirection;
                if (startRotate != null)
                {
                    startRotate(currentDirection);
                }
            }
            float magnitude = (target.position - unit.transform.position).magnitude;
            if (magnitude < stopDistance)
            {
                if (destination != null)
                {
                    destination();
                }
                rg.velocity = Vector3.zero;
                this.DOKill();
                unit.isFree = false;
                target = null;

            }
            else
            {
                this.DOKill();
                rg.DOMove(target.position, magnitude / (skillPower*0.5f));

            }
        }
        else
        {
            rg.velocity = new Vector2(currentDirection * skillPower, fly ? 0 : Physics.gravity.y);
        }
       
        if (move != null)
        {
            move(Mathf.Abs(currentDirection));
        }
    }

    public override void Setup(Skill skill)
    {
        base.Setup(skill);
        waterId = (skill as MoveAI).waterId;
        playersLayer = (skill as MoveAI).playersLayer;
        stopDistance =  (skill as MoveAI).stopDistance;
        aggroRadius = (skill as MoveAI).aggroRadius;
    }
}
