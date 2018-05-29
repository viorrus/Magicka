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
        if (shoot != null)
        {
            shoot();
        }
    }

    public void Land() { 
}
}


