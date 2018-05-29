using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class UnitProjectiles : UnitObject {

    private void OnTriggerEnter(Collider other)
    {
        if (triggerIN != null)
        {
            triggerIN(other);
        }

        if (!other.GetComponent<UnitProjectiles>())
        {
            if (deathAct != null)
            {
                deathAct();
            }

            Destroy(gameObject);
        }
    }



}
