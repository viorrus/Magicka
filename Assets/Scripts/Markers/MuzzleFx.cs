using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFx : MonoBehaviour {

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
