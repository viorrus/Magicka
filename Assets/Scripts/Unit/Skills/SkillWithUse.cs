using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

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

    public override void Setup(Skill skill)
    {
        manacost = skill.manacost;
        castTime = skill.castTime;
        cdTime = skill.cdTime;
        durationTime = skill.durationTime;
        castRange = skill.castRange;
        skillPower = skill.skillPower;//использовать по умолчанию
        usePriority = skill.usePriority;
        isAlwaysUse = skill.isAlwaysUse;
        skillImage = skill.skillImage;
    }
}