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



    [Header("Crystals")]
    [SerializeField]
    List<GameObject> crystals;

    [Header("DragNdrop")]
    [SerializeField]
    Animator m_shipAnimator;
    [SerializeField]
    public Gem gem;
    [SerializeField]
    public GameObject innerRing;
    [SerializeField]
    public GameObject door;
    [SerializeField]
    public Material material;

    private bool m_rotate=true;

    public void Start()
    {
        gem.onHit += OnGemHit;
        material.SetFloat("_EmissionScale", 0);
        foreach (GameObject crystal in crystals)
        {
            crystal.SetActive(false);
        }

        crystals[crystals.Count - 1].GetComponent<Crystal>().OnExplode += (Crystal c) => { m_shipAnimator.SetTrigger("Break"); };

    }

    void OnGemHit()
    {
        if (innerRing.transform.eulerAngles.z < 60 || innerRing.transform.eulerAngles.z > 300)
        {
            //OpenDoor();
            this.enabled = false;
            m_rotate = false;
            gem.onHit -= OnGemHit;
            material.SetFloat("_EmissionScale", 6);

            gem.transform.DOLocalMoveZ(gem.transform.localPosition.z - 0.04f, delayTime).OnComplete(OpenDoor);

        }
        else
        {
            m_rotate = false;

            innerRing.transform.DORotate(innerRing.transform.eulerAngles - new Vector3(0, 0, 10), 0.6f).OnComplete(()=> { m_rotate = true; });
        }
        
    }

    void OpenDoor()
    {
        foreach (GameObject crystal in crystals)
        {
            crystal.SetActive(true);
        }
        door.transform.DOLocalRotate(new Vector3(-100, 0, 0), 1.8f).SetEase(Ease.InQuad).OnComplete(CurveWallker.instance.StopMovement);
    }

    void Update () {
        
        if (m_rotate)
            innerRing.transform.eulerAngles += new Vector3(0, 0, speed);
	}
}
