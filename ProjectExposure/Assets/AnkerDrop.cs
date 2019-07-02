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
    [SerializeField]
    private Vector3 m_desiredRotation;



    public void AnkerGO()
    {
        Tweener t = m_anker.gameObject.transform.DOLocalMove(m_desiredPosition, m_duration);
        m_anker.gameObject.transform.DOLocalRotate(m_desiredRotation, m_duration);
        Tweener camShake = m_player.transform.DOShakePosition(m_camShakeDuration, m_camShakeStrength); 
        t.SetEase(Ease.InQuad);
        Sequence s = DOTween.Sequence();
        s.Append(t);
        s.Append(camShake);
        
    }
    
}
