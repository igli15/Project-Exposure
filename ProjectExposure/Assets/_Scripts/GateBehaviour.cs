using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GateBehaviour : MonoBehaviour {

    [SerializeField]
    public Crystal crystalLeft;
    [SerializeField]
    public Crystal crystalMidle;
    [SerializeField]
    public Crystal crystalRight;

    [SerializeField]
    public GameObject coinlLeft;
    [SerializeField]
    public GameObject coinMidle;
    [SerializeField]
    public GameObject coinRight;

    [SerializeField]
    public GameObject doorLeft;

    [SerializeField]
    public GameObject doorRight;

    public int coinCount = 0;

    public void Start()
    {
        crystalLeft.OnHit.AddListener(()=>RotateCoin(coinlLeft));
        crystalMidle.OnHit.AddListener(() => RotateCoin(coinMidle));
        crystalRight.OnHit.AddListener(() => RotateCoin(coinRight));
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
        coin.transform.DOLocalRotateQuaternion(Quaternion.Euler(0,180,0),1);
        OpenDoor();
    }

    public void OpenDoor()
    {
        if (coinCount >= 3)
        {
            doorLeft.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 100, 0), 3);
            doorRight.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, -100, 0), 3);
        }

    }

}
