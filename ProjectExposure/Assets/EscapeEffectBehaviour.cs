using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EscapeEffectBehaviour : MonoBehaviour {

	
	void Start () {
        transform.localScale = Vector3.zero;

        transform.DOScale(new Vector3(1, 1, 1), 1.5f).OnComplete( ()=> { transform.localScale = Vector3.zero; } );
	}

    public void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

}
