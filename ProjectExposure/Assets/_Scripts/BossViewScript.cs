using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossViewScript : MonoBehaviour {


    private void Start()
    {
        CurveWallker.instance.lookForward = false;
    }
    void Update () {
        CurveWallker.instance.transform.LookAt(transform);	
	}
}
