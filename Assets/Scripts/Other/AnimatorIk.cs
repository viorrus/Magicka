using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorIk : MonoBehaviour {

    protected Animator animator;

    public bool ikActive = false;
    public Transform rootBone = null;
    public Transform lookObj = null;
    System.Action shoot;


    public void Init(Transform target, Animator animator, Transform bone, System.Action shoot)
    {
        this.animator = animator;
        rootBone = bone;
        lookObj = target;
        this.shoot += shoot;
    }

    public void SetFlag(bool flag)
    {
        ikActive = flag;
    }

    public void Shoot()
    {
        var temp = GetComponent<UnitPlayerObject>();
        if (!temp)
        {
            if (shoot != null)
            {
                shoot();
            }
        }
        else
        {
            if (temp.isLocalPlayer)
            {
                if (shoot != null)
                {
                    shoot();
                }
            }
        }
    }

    public void StepL()
    {
        SoundManager.Instance.PlaySound("stepL");
    }

    public void StepR()
    {
        SoundManager.Instance.PlaySound("stepR");
    }

    public void Jump()
    {
        SoundManager.Instance.PlaySound("jump");
    }


    public void Land()
    {
       
    }
}


