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

    [SerializeField]
    private float m_openingDuration = 1.8f;

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
    public bool alreadySolved = false;

    private bool m_rotate=true;

    public void Start()
    {
        gem.onHit += OnGemHit;
        material.SetFloat("_EmissionScale", 0);
        foreach (GameObject crystal in crystals)
        {
            crystal.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            crystal.SetActive(false);

        }

        crystals[crystals.Count - 1].GetComponent<Crystal>().OnExplode += (Crystal c) => { m_shipAnimator.SetTrigger("Break");};

    }

    void OnGemHit()
    {
        if (innerRing.transform.eulerAngles.z < 20 || innerRing.transform.eulerAngles.z > 340)
        {
            alreadySolved =true;
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

            material.SetFloat("_EmissionScale", 6);
            innerRing.transform.DORotate(innerRing.transform.eulerAngles - new Vector3(0, 0, 0), 0.8f).OnComplete(()=> { m_rotate = true; material.SetFloat("_EmissionScale", 0); });
        }
        
    }

    void OpenDoor()
    {

        
        door.transform.DOLocalRotate(new Vector3(-100, 0, 0), m_openingDuration).SetEase(Ease.InQuad).OnComplete(
            ()=> {
                
                Sequence crystalSpawn = DOTween.Sequence();
                //crystalSpawn.isBackwards = true;
                int crystalCount = 0;
                foreach (GameObject crystal in crystals)
                {
                    crystalCount++;
                    crystal.SetActive(true);
                    if (crystalCount == crystals.Count)
                    {
                        Tween crystalApperance = crystal.transform.DOScale(15, speed);
                        crystalSpawn.Append(crystalApperance);
                    }
                    else
                    {
                        Tween crystalApperance = crystal.transform.DOScale(4, speed);
                        crystalSpawn.Append(crystalApperance);
                    }
                }
            });
        Invoke("ContinueMoving", m_openingDuration+3.0f);
        
    }

    void ContinueMoving()
    {
        CurveWallker.instance.StartMovement();
    }

    void Update () {
        
        if (m_rotate)
            innerRing.transform.eulerAngles += new Vector3(0, 0, 0.5f);
	}
}
