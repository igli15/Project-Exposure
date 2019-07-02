using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GateSimpleBehaviour : MonoBehaviour {

    public Gem gem;
    public GameObject door;
    [SerializeField]
    public GameObject collider;
    public HintPanel hint;

    public void Start()
    {
        gem.onHit += OpenDoor;
    }

    void OpenDoor()
    {
        gem.onHit -= OpenDoor;
        collider.SetActive(false);
        enabled = false;
        door.transform.DOLocalRotate(new Vector3(-100, 0, 0), 1.8f).SetEase(Ease.InQuad).OnComplete(CurveWallker.instance.StartMovement);
        hint.Hide();

    }
}
