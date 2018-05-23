using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour {
    public UnitObject source;
    public Skill skillSource;
    public float startValue;
    public UnitObject unit;
    public GameObject prefab;

    public virtual void Init(UnitObject initUnit, Skill initSkill, GameObject initPrefab)
    {
        prefab = initPrefab;
        skillSource = initSkill;
        source = initUnit;
    }

    public virtual void DoEffect()
    {
        if(prefab != null)
        {
            prefab = Instantiate(prefab, transform);
        }
      
        unit = GetComponent<UnitObject>();
    }

   public virtual void OnDestroy()
    {
        Destroy(prefab);        
    }
}
