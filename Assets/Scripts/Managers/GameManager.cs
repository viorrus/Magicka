using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    [SyncVar(hook = "SetLocalPause")]
    public bool isPause;

    [SyncVar]
    public int botCount;

    public int maxBotCount;

    [Command]
    public void CmdSetPause()
    {
        isPause = !isPause; 
    }

    void SetLocalPause(bool pause)
    {
        isPause = pause;
        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

  public bool CheckSpawn()
    {
        return botCount < maxBotCount;
    }
	
}
