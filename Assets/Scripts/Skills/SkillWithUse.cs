using System.Collections;
using UnityEngine;

public class SkillWithUse : Skill
{
    public override void Init()
    {

    }

    public override void CheckSkill()
    {
        if (timer > 0 )
        {
            if (SpecificCheckSkill())
            {
                isReady = true;
            }
        }
    }

    public virtual bool SpecificCheckSkill()
    {
        return true;
    }

    public override void UseSkill()
    {
        isReady = false;
        timer -= cdTime;  
    }

    public virtual IEnumerator SkillDuration()
    {
        yield return new WaitForSeconds(durationTime);
    }
}