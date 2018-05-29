using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PowerUpDamage : PowerUp {

    public GameObject powerUpEffect;
    Collider tempCollider;


    public override void TriggerEnter(Collider other)
    {
        base.TriggerEnter(other);
        var temp = other.GetComponent<UnitObject>();
        tempCollider = other;
        CmdSpawn();
    }

    [Command]
    void  CmdSpawn()
    {
        NetworkServer.Spawn(powerUpEffect.InstantiateMe(tempCollider.transform.position, Quaternion.identity, tempCollider.transform));
    }


    void OnDestroy()
    {
        unit.triggerIN -= TriggerEnter;
    }
}
