using System.Collections.Generic;
using UnityEngine;

public class SkillMasterUnit : SkillMaster
{
  

    public override void Start()
    {
        unit = GetComponent<UnitObject>();
        unit.death += DestoyAllSkills;
        if(skillList == null)
        {
            skillList = new List<Skill>();
        }
       
        foreach(Skill sk in unit.unitBase.skills)
        {
            skillList.Add(sk.InstantiateMe(transform));
            skillList[skillList.Count - 1].unit = unit;
            skillList[skillList.Count - 1].Init();
        }
        unit.unitBase.skills = skillList;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void DestoyAllSkills()
    {
        base.DestoyAllSkills();
    }

    public override void CullDownUpdate()
    {
        base.CullDownUpdate();
    }

    public override void SelectSkill()
    {
        base.SelectSkill();
    }
}