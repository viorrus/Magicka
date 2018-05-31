using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkBehaviour {
    private static CustomNetworkManager instance;
    public static CustomNetworkManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CustomNetworkManager>();
            }
            return instance;
        }
    }

    public void SpawnObjectOnServer(GameObject obj, Vector3 pos, Quaternion rot)
    {
        CmdSpawn(obj, pos,rot);
    }


    [Command]
    void CmdSpawn(GameObject obj, Vector3 pos, Quaternion rot)
    {
        Instantiate(obj, pos, rot);
        NetworkServer.Spawn(Instantiate(obj, pos, rot));
    }

}
