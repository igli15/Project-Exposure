using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GateSimpleBehaviour : MonoBehaviour {

    [SerializeField]
    public Gem gem;
    [SerializeField]
    public GameObject door;

    public void Start()
    {
        gem.onHit += OpenDoor;
    }

    void OpenDoor()
    {
        door.transform.DOLocalRotate(new Vector3(-100, 0, 0), 1.8f).SetEase(Ease.InQuad).OnComplete(CurveWallker.instance.StartMovement);
    }
}
