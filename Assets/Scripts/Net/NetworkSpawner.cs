using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSpawner : NetworkBehaviour {
    [SyncVar]
    public NetworkInstanceId parentNetId;


    public override void OnStartClient()
    {
        GameObject parentObject = ClientScene.FindLocalObject(parentNetId);

        if (parentObject != null)
        {
            transform.SetParent(parentObject.transform);
            transform.localScale = Vector3.one;
        }
    }

}
