using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossViewScript : MonoBehaviour {

    public static BossViewScript instance;

    [SerializeField]
    List<TentacleBehaviour> m_tentacles;
    [SerializeField]
    GameObject m_crystal;
    private void Start()
    {
        instance = this;

        foreach (TentacleBehaviour tentacle in m_tentacles)
        {
            tentacle.onEnd += OnTentacleEnd;
            tentacle.Initialize();
            tentacle.enabled = false;
        }
        ActivateNextTentacle();
    }

    public void ActivateNextTentacle()
    {
        if (m_tentacles.Count == 0) return;
        m_tentacles[0].enabled = true;
        m_tentacles[0].ActivateTentacle(m_crystal);
        m_tentacles.RemoveAt(0);
    }

    public void OnTentacleEnd(TentacleBehaviour tentacle)
    {
        ActivateNextTentacle();
    }

    void Update () {
        CurveWallker.instance.lookForward = false;
        CurveWallker.instance.transform.LookAt(transform);
	}
}
