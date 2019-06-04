using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GateBehaviour : MonoBehaviour {

    [Header("Crystals")]
    [SerializeField]
    public Crystal crystalLeft;
    [SerializeField]
    public Crystal crystalMidle;
    [SerializeField]
    public Crystal crystalRight;

    [Header("Coins")]
    [SerializeField]
    public GameObject coinlLeft;
    [SerializeField]
    public GameObject coinMidle;
    [SerializeField]
    public GameObject coinRight;

    [Header("Rings")]
    [SerializeField]
    public GameObject ringLeft1;
    [SerializeField]
    public GameObject ringRight1;
    [SerializeField]
    public GameObject ringLeft2;
    [SerializeField]
    public GameObject ringRight2;
    [SerializeField]
    public GameObject ringLeft3;
    [SerializeField]
    public GameObject ringRight3;

    [Header("MainDoors")]
    [SerializeField]
    public GameObject doorLeft;
    [SerializeField]
    public GameObject doorRight;

    [HideInInspector]
    public int coinCount = 0;

    public void Start()
    {
        crystalLeft.OnHit.AddListener(()=>RotateCoin(coinlLeft));
        crystalMidle.OnHit.AddListener(() => RotateCoin(coinMidle));
        crystalRight.OnHit.AddListener(() => RotateCoin(coinRight));

        crystalLeft.OnHit.AddListener(() => RotateRing(ringLeft1,ringRight1));
        crystalMidle.OnHit.AddListener(() => RotateRing(ringLeft2, ringRight2));
        crystalRight.OnHit.AddListener(() => RotateRing(ringLeft3, ringRight3));

        ringLeft1.transform.Rotate(new Vector3(0, 0, 45));
        ringRight1.transform.Rotate(new Vector3(0, 0, 45));

        ringLeft2.transform.Rotate(new Vector3(0, 0, 75));
        ringRight2.transform.Rotate(new Vector3(0, 0, 75));

        ringLeft3.transform.Rotate(new Vector3(0, 0, 95));
        ringRight3.transform.Rotate(new Vector3(0, 0, 95));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) crystalLeft.OnHit.Invoke();
        if (Input.GetKeyDown(KeyCode.S)) crystalMidle.OnHit.Invoke();
        if (Input.GetKeyDown(KeyCode.D)) crystalRight.OnHit.Invoke();
    }

    public void RotateCoin(GameObject coin)
    {
        coinCount++;
        Debug.Log("RotateCoin: "+coin.name);
        //coin.transform.DORotate(new Vector3(0, 180, 0),1);
        Tween rotation=coin.transform.DOLocalRotateQuaternion(Quaternion.Euler(0,180,0),1);
        
        //OpenDoor();
    }

    public void RotateRing(GameObject ringLeft, GameObject ringRight)
    {
        Tween rotationLeft = ringLeft.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 1);
        Tween rotationRight = ringRight.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 1);
        rotationLeft.onComplete += OpenDoor;
    }

    public void OpenDoor()
    {
        if (coinCount >= 3)
        {
            doorLeft.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, -100, 0), 4).SetEase(Ease.InSine);
            doorRight.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 100, 0), 4).SetEase(Ease.InSine);
        }

    }

}
