using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class UnitPlayerObject : UnitObject
{

    [SyncVar(hook = "UpdateFrag")]
    public int fragCount;
   



    void UpdateFrag(int value)
    {
        fragCount = value;

        CanvasManager.Instance.SetScoreText(fragCount);
    }



    public override void OnStartLocalPlayer()
    {
        CmdSetID();

        var temp = FindObjectsOfType<NetworkStartPosition>();
        transform.position = temp[Random.Range(0, temp.Length)].transform.position;
        if (isLocalPlayer)
        {
            var tempObjects = GetComponent<LocalObjects>();
            foreach (GameObject go in tempObjects.localGroup)
            {
                go.SetActive(true);
            }
            UpdateFrag(PlayerPrefs.GetInt("FragCount", 0));
        }

    }

    [Command]
    void CmdSetID()
    {
        ID = GetComponent<NetworkIdentity>().netId;
    }


    private void OnDestroy()
    {
        if (isLocalPlayer)
        {
            PlayerPrefs.SetInt("FragCount", fragCount);
        }

    }

}
