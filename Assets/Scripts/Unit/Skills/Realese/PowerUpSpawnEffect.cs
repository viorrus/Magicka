using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PowerUpSpawnEffect : PowerUp {

    public GameObject powerUpEffect;
    public GameObject infoObject;
    Collider tempCollider;


    public override void TriggerEnter(Collider other, UnitObject source)
    {
        base.TriggerEnter(other,source);
        var temp = other.GetComponent<UnitObject>();
        if (!temp.GetComponentInChildren<Effect>())
        {
            (source as UnitCollisions).DeActivate();
            tempCollider = other;
            CmdSpawn();
        }
      
    }

    [Command]
    void  CmdSpawn()
    {
        if (powerUpEffect != null)
        {
            var tempSpawn = powerUpEffect.InstantiateMe(tempCollider.transform.position, Quaternion.identity, tempCollider.transform);
            tempSpawn.GetComponent<NetworkSpawner>().parentNetId = tempCollider.GetComponent<NetworkIdentity>().netId;
            NetworkServer.Spawn(tempSpawn);
        }
        if (infoObject != null)
        {
            NetworkServer.Spawn(infoObject.InstantiateMe(tempCollider.transform.position, Quaternion.identity, tempCollider.transform));
        }
        
    }


    void OnDestroy()
    {
        unit.triggerIN -= TriggerEnter;
    }
}
