using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class UnitPlayerObject : UnitObject {


    public override void Start()
    {
        base.Start();

    }


    public override void OnStartLocalPlayer()
    {
        ID = NetworkManager.singleton.numPlayers;
        
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

    public override void GetDamage(float damage)
    {
        GetDamage(ID ,damage);
       
    }

    void GetDamage(int id, float damage)
    {
        if (damageAct != null)
        {
            damageAct(damage);
        }
    }


    #region UnetToggle

    [Command]
    public void CmdSetAttack()
    {
        
    }



    #endregion

}
