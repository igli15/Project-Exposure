using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class TentacleBehaviour : MonoBehaviour {

    [SerializeField]
    [Range(0,1)]
    public float range = 0.1f;

    [SerializeField]
    public List<Transform> placeholders;

    int m_crystalCount;
    int m_currentCount;

    public Action<TentacleBehaviour> onEnd;

	public void Start () {

    }

    public void Initialize()
    {
        m_crystalCount = placeholders.Count;
        m_currentCount = m_crystalCount;

    }

    void Update () {
        Vector3 distance = ( BossViewScript.instance.transform.position- CurveWallker.instance.transform.position );
        distance *= range;
        Vector3 buffer = CurveWallker.instance.transform.position + distance;
        buffer.y = transform.position.y;
        transform.position = buffer;
        transform.LookAt(CurveWallker.instance.transform);
    }

    void OnCrystalExplode(Crystal c)
    {
        m_currentCount--;
        if (m_currentCount <= 0)
        {
            Debug.Log("OVER");
            HideTentacle();
        }
    }

    public void ActivateTentacle(GameObject crystal)
    {
        foreach (Transform t in placeholders)
        {
            GameObject newCrytsal = GameObject.Instantiate(crystal, t.position, t.rotation, t);
            Crystal c = newCrytsal.GetComponent<Crystal>();
            c.OnExplode += OnCrystalExplode;

            /*c.SetColor(new Color(
                UnityEngine.Random.Range(0, 255)/255.0f, UnityEngine.Random.Range(0, 255) / 255.0f,
                UnityEngine.Random.Range(0, 255) / 255.0f, 1));*/
            c.SetColor(new Color(1,0,0, 1));


        }
        transform.position = new Vector3(transform.position.x, -40, transform.position.z);
        transform.DOMoveY(6, 1);
    }

    public void HideTentacle()
    {
        enabled = false;
        transform.DOLocalMoveY(-40, 1).OnComplete(() => { gameObject.SetActive(false); if (onEnd != null) onEnd(this); });
    }
}
