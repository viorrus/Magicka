using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetHud : NetworkBehaviour {

    public int port;
    public InputField ipText;
    public Text myIpText;

    private void Start()
    {
        if (ipText)
        {
            ipText.textComponent.text = PlayerPrefs.GetString("ip", NetworkManager.singleton.networkAddress);
        }
       
        if (myIpText)
        {
            myIpText.text = string.Format("Your`s Ip Addres: {0}", Network.player.ipAddress);
        }
     
    }

    public void SetupAndStartHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void SetupAndStartClient()
    {
        SetPort();
        SetIP();
       
        try
        {
           NetworkManager.singleton.StartClient();
        }
        catch
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(ipText.textComponent.DOText("Try again", 0.5f))
                .SetDelay(0.5f)
                .Append(ipText.textComponent.DOText("localhost", 0.5f));
        } 
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = port;
    }

    void SetIP()
    {
        NetworkManager.singleton.networkAddress = ipText.text;
        PlayerPrefs.SetString("ip", NetworkManager.singleton.networkAddress);
        if(NetworkManager.singleton.networkAddress == "")
        {
            NetworkManager.singleton.networkAddress = "localhost";
        }
    }

    public void Close()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Disconnect()
    {
        if (isServer)
        {
            NetworkManager.singleton.StopHost();
        }
        else
        {
            Network.Disconnect(2);
            NetworkManager.singleton.StopClient();
        }

    }

}
