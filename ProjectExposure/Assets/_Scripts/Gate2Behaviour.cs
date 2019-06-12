using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gate2Behaviour : MonoBehaviour {
    [Header("General")]
    [SerializeField]
    public float speed = 0.3f;
    [SerializeField]
    public float delayTime = 3;

    [Header("DragNdrop")]
    [SerializeField]
    public Gem gem;
    [SerializeField]
    public GameObject innerRing;
    [SerializeField]
    public GameObject door;
    [SerializeField]
    public Material material;

    private bool m_rotate=false;

    public void Start()
    {
        gem.onHit += OnGemHit;
        material.SetFloat("_EmissionScale", 0);
    }

    void OnGemHit()
    {
        if ( innerRing.transform.eulerAngles.z<60 || innerRing.transform.eulerAngles.z > 300)
        {
            //OpenDoor();
            this.enabled = false;
            m_rotate = false;
            gem.onHit -= OnGemHit;
            material.SetFloat("_EmissionScale", 6);

            gem.transform.DOLocalMoveZ(gem.transform.localPosition.z-0.04f, delayTime).OnComplete (OpenDoor);
            return;
        }
        m_rotate = !m_rotate;
    }

    void OpenDoor()
    {
        door.transform.DOLocalRotate(new Vector3(-100, 0, 0), 1.8f).SetEase(Ease.InQuad).OnComplete(CurveWallker.instance.StartMovement);
    }

    void Update () {
        Debug.Log("Z" + innerRing.transform.eulerAngles.z);
        if (m_rotate)
            innerRing.transform.eulerAngles += new Vector3(0, 0, speed);
	}
}
