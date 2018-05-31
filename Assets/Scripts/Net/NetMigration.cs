using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetMigration : NetworkMigrationManager {

    //Вызывается на стороне клиента когда он теряет связь с хостом\сервером
    protected override void OnClientDisconnectedFromHost(NetworkConnection conn, out SceneChangeOption sceneChange)
    {
        Debug.Log("Migration >> On Client Disconnected From Host");
        base.OnClientDisconnectedFromHost(conn, out sceneChange);

        UnityEngine.Networking.NetworkSystem.PeerInfoMessage NewHostInfo = new UnityEngine.Networking.NetworkSystem.PeerInfoMessage();
        bool _youAreNewHost = false;

        FindNewHost(out NewHostInfo, out _youAreNewHost);

        //Может ли данный игрок стать хостом
        if (_youAreNewHost == true)
        {
            BecomeNewHost(7777);
        }
        else
        {
            newHostAddress = NewHostInfo.address;
            Reset(oldServerConnectionId);
            NetworkManager.singleton.networkAddress = newHostAddress;
            NetworkManager.singleton.client.ReconnectToNewHost(newHostAddress, NetworkManager.singleton.networkPort);
        }
    }

}
