using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnkerDrop : MonoBehaviour {

	[SerializeField]
    private GameObject m_anker;
    private float m_duration = 2f;

    public void OnDestroy()
    {
        Tweener t = m_anker.gameObject.transform.DOMove(new Vector3(-111.72f, -23.76f, -22.94f), m_duration);
        t.SetEase(Ease.InQuad);
        m_anker.gameObject.transform.DORotate(new Vector3(391.904f, -60.286f, 90), m_duration);
    }
    
}
