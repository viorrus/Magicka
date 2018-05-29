using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class UnitCollisions : UnitObject {

    public string ownerID;

    private void OnTriggerEnter(Collider other)
    {
        if (triggerIN != null)
        {
            triggerIN(other);
        }

        if (!other.GetComponent<UnitCollisions>())
        {
            if (deathAct != null)
            {
                deathAct();
            }

            Destroy(gameObject);
        }
    }



}
