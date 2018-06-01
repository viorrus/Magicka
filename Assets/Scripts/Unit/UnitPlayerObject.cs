using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class UnitPlayerObject : UnitObject
{

    [SyncVar(hook = "UpdateFrag")]
    public int fragCount;

    public override void Awake()
    {
        unitBase = unitBase.InstantiateMe(transform);

        List<Stat> temp = new List<Stat>(GetComponentsInChildren<Stat>());
        temp.ForEach(x =>
        {
            if (unitBase.GetStatByName(x.stringId) != null)
            { x.Setup(unitBase.GetStatByName(x.stringId)); }
        });
        unitBase.stats = new List<Stat>(temp);
        gameObject.AddComponent<SkillMasterUnit>();
    }


    void UpdateFrag(int value)
    {
        fragCount = value;

        CanvasManager.Instance.SetScoreText(fragCount);
    }



    public override void OnStartLocalPlayer()
    {
      

        var temp = FindObjectsOfType<NetworkStartPosition>();
        transform.position = temp[Random.Range(0, temp.Length)].transform.position;
        if (isLocalPlayer)
        {
            CmdSetID();
            UpdateFrag(0);
            var tempObjects = GetComponent<LocalObjects>();
            foreach (GameObject go in tempObjects.localGroup)
            {
                go.SetActive(true);
            }
         
        }

    }

    [Command]
    void CmdSetID()
    {
        ID = GetComponent<NetworkIdentity>().netId;
    }


    private void OnDestroy()
    {
    }

}
