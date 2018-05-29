using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour {

    public float duration;
    public float powerUpValue;
   



    private void Start()
    {
        Destroy(gameObject, duration);
        (GetComponentInParent<UnitPlayerObject>().unitBase.GetSkillByName("Attack") as ProjectilesAttack).spawn += AddDamage;
    }

  
    void AddDamage(UnitCollisions unit)
    {
      unit.unitBase.GetStatByName("Damage").AddValue(powerUpValue);
    }

    private void OnDestroy()
    {
        (GetComponentInParent<UnitPlayerObject>().unitBase.GetSkillByName("Attack") as ProjectilesAttack).spawn -= AddDamage;
        Debug.Log("ImDestroy");
    }


}
