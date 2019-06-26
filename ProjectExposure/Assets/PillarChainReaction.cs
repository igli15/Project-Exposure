using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PillarChainReaction : MonoBehaviour {

    [SerializeField]
    private GameObject m_pillar;
    [SerializeField]
    private GameObject m_player;
    [SerializeField]
    private float m_duration = 10f;
    [SerializeField]
    private float m_camShakeStrength = 1.2f;
    [SerializeField]
    private float m_camShakeDuration = 0.3f;
    [SerializeField]
    private Vector3 m_desiredPosition;
    [SerializeField]
    private Vector3 m_desiredRotation;
    [SerializeField]
    private Ease ease;
    [SerializeField]
    private bool breakable = false;
    [SerializeField]
    private GameObject m_pillarPieceColection;
    private void OnDestroy()
    {
        if (!breakable) ;
        Tweener t = m_pillar.gameObject.transform.DORotate(m_desiredRotation, m_duration, RotateMode.Fast);
        if(m_desiredPosition != null)
        m_pillar.gameObject.transform.DOMove(m_desiredPosition, m_duration);
        Tweener camShake = m_player.transform.DOShakePosition(m_camShakeDuration, m_camShakeStrength);
        t.SetEase(ease);
        //m_pillar.gameObject.transform.DOLocalRotate(m_desiredRotation, m_duration);
        Sequence s = DOTween.Sequence();
        s.Append(t);
        s.Append(camShake);
        
        if (breakable)
        {
            //s.onComplete += () => { ActivateRigidBody(); };
            ActivateRigidBody();
        }

    }
    

    private void ActivateRigidBody()
    {
        Debug.Log("Sojnasodnsadnsaodnsaodas");
        int pillarPieces = m_pillarPieceColection.gameObject.transform.childCount;
        
        for (int i = 0; i < pillarPieces; i++ )
        {
            m_pillarPieceColection.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
        }
       
    }

}
