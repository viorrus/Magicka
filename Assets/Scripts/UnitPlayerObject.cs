using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class UnitPlayerObject : UnitObject {

    [SyncVar(hook ="UpdateFrag")]
    public int fragCount;
    public string idLastHit;



    void UpdateFrag(int value)
    {
        fragCount = value;
    }


    public override void OnStartLocalPlayer()
    {
        ID = Network.player.ipAddress;
        
       var temp =  FindObjectsOfType<NetworkStartPosition>();
        transform.position = temp[Random.Range(0, temp.Length)].transform.position;
        if (isLocalPlayer)
        {
            var tempObjects = GetComponent<LocalObjects>();
            foreach(GameObject go in tempObjects.localGroup)
            {
                go.SetActive(true);
            }
        }

    }

    public override void GetDamage(float damage, string id)
    {
        GetDamage(id ,damage);
       
    }

    void GetDamage(string id, float damage)
    {
        if (damageAct != null)
        {
            damageAct(damage,id);
        }
    }


    #region UnetToggle

    [Command]
    public void CmdSetAttack()
    {
        
    }



    #endregion

}
