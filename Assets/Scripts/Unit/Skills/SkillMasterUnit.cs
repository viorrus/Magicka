using System.Collections.Generic;
using UnityEngine;

public class SkillMasterUnit : SkillMaster
{
  

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (isLocalPlayer)
        {
            CullDownUpdate();
            SelectSkill();
        }
       
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