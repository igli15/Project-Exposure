using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnkerDrop : MonoBehaviour {

	[SerializeField]
    private GameObject m_anker;
    [SerializeField]
    private GameObject m_player;
    [SerializeField]
    private float m_duration = 2f;
    [SerializeField]
    private float m_camShakeStrength = 1f;
    [SerializeField]
    private float m_camShakeDuration = 0.3f;
    [SerializeField]
    private Vector3 m_desiredPosition;
    


    public void OnDestroy()
    {
        /*Tweener t = m_anker.gameObject.transform.DOMove(m_desiredPosition, m_duration);
        Tweener camShake = m_player.transform.DOShakePosition(m_camShakeDuration, m_camShakeStrength); 
        t.SetEase(Ease.InQuad);
        m_anker.gameObject.transform.DORotate(new Vector3(391.904f, -60.286f, 90), m_duration);
        Sequence s = DOTween.Sequence();
        s.Append(t);
        s.Append(camShake);*/
        
    }
    
}
