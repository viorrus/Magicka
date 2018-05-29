using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SkillMaster : NetworkBehaviour
{
    public UnitObject unit;
    public List<Skill> skillList;
    public Skill selectedSkill;

    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        CullDownUpdate();
        SelectSkill();
    }

    public virtual void DestoyAllSkills()
    {
        foreach (Skill skill in skillList)
        {
            Destroy(skill);
        }
        Destroy(this);
    }

    public virtual void CullDownUpdate()
    {
        for (int i = 0; i < skillList.Count; i++)
        {
            if (skillList[i].timer <= 0)
            {
                skillList[i].timer += Time.deltaTime;
            }
        }
    }

    public virtual void SelectSkill()
    {
        List<Skill> readyList = new List<Skill>();
        for (int i = 0; i < skillList.Count; i++)
        {
            skillList[i].CheckSkill();
            if (!skillList[i].isAlwaysUse && skillList[i].isReady)
            {
                readyList.Add(skillList[i]);
            }
            else if (skillList[i].isAlwaysUse)
            {
                skillList[i].UseSkill();
            }
        }
        if (readyList.Count > 0)
        {
            int min = 0;
            int select = 0;
            for (int i = 0; i < readyList.Count; i++)
            {
                if (readyList[i].usePriority > min)
                {
                    select = i;
                }
            }
            selectedSkill = readyList[select];
        }
        else
        {
            selectedSkill = null;
        }
        if(selectedSkill is SkillWithUse)
        {
            selectedSkill.UseSkill();
        }
    }
}