using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Effect : NetworkBehaviour {

    public float duration;
    public float powerUpValue;

    public virtual void Start()
    {
        Destroy(gameObject, duration);
    }

    public virtual void OnDestroy()
    {
    }


}
