using System.Collections.Generic;
using UnityEngine;

public class SkillMasterBlock : SkillMaster
{
   

   public override void Start()
    {
        unit = GetComponent<BlockObject>();
        unit.deathAct += DestoyAllSkills;
        if(skillList == null)
        {
            skillList = new List<Skill>();
        }

        skillList.AddRange(unit.GetComponentsInChildren<Skill>());

        skillList.ForEach(x => { x.Setup(unit.unitBase.GetSkillByName(x.skillName));x.unit = unit; x.Init(); });   
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