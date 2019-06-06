using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RotationZone : MonoBehaviour {
    [SerializeField]
    public Transform target;
    [SerializeField]
    public float durationOfrotation = 1;

    CurveWallker m_player;
    bool m_lookAtTarget = false;

    void Start ()
    {
        m_player = CurveWallker.instance;
    }
	
	void Update ()
    {
        if (m_lookAtTarget) m_player.transform.LookAt(target);
    }

    public void RotateTowards(Vector3 targetPosition,float time)
    {
        m_player = CurveWallker.instance; //just in case

        m_player.lookForward = false;

        Vector3 futurePosition = m_player.GetPositionIn(time);
        Vector3 newTargetPosition = (targetPosition-futurePosition) + m_player.transform.position;

        m_player.transform.DOLookAt(newTargetPosition, time).onComplete+=()=> { m_lookAtTarget = true; };
    }

    public void RotateBackToSpline(float time)
    {
        m_player = CurveWallker.instance; //just in case
        m_lookAtTarget = false;

        Vector3 futurePosition = m_player.GetPositionIn(time);
        Vector3 newTargetPosition = m_player.transform.position + m_player.GetDirectionIn(time)*5;

        m_player.transform.DOLookAt(newTargetPosition, time).onComplete += () => { m_player.lookForward = true; };
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            RotateTowards(target.position, durationOfrotation);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            RotateBackToSpline(durationOfrotation);
    }


}
