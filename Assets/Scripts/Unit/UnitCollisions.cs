using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class UnitCollisions : UnitObject {

    public NetworkInstanceId ownerID;

    private void OnTriggerEnter(Collider other)
    {
        if (triggerIN != null)
        {
            triggerIN(other, this);
        }

      
    }

    public void DeActivate()
    {
            if (deathAct != null)
            {
                deathAct();
            }

            Destroy(gameObject);
    }

}
