using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class TentacleBehaviour : MonoBehaviour {

    [SerializeField]
    [Range(0,1)]
    public float range = 0.1f;
    [SerializeField]
    List<Crystal> crystals;
    int m_crystalCount;
    int m_currentCount;

    public Action<TentacleBehaviour> onEnd;

	void Start () {
        foreach (Crystal c in crystals)
        {
            c.OnExplode += OnCrystalExplode;
        }

        m_crystalCount = crystals.Count;
        m_currentCount = m_crystalCount;
        
    }
	
	void Update () {
        Vector3 distance = ( BossViewScript.instance.transform.position- CurveWallker.instance.transform.position );
        distance *= range;
        Vector3 buffer = CurveWallker.instance.transform.position + distance;
        buffer.y = transform.position.y;
        transform.position = buffer;
        transform.LookAt(CurveWallker.instance.transform);
    }

    void OnCrystalExplode(Crystal c)
    {
        m_currentCount--;
        if (m_currentCount <= 0)
        {
            Debug.Log("OVER");
            HideTentacle();
        }
    }

    public void ActivateTentacle()
    {
        transform.position = new Vector3(transform.position.x, -5, transform.position.z);
        transform.DOMoveY(6, 1);
    }

    public void HideTentacle()
    {
        enabled = false;
        transform.DOLocalMoveY(-10, 1).OnComplete(() => { gameObject.SetActive(false); if (onEnd != null) onEnd(this); });
    }
}
