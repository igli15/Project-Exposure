using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSlide : MonoBehaviour {

    [SerializeField]
    private GameObject m_waterStream;

    [SerializeField]
    private CurveBinder m_optionalPath;

    [SerializeField]
    private CurveBinder m_standardPath;

    [SerializeField]
    public Gem gem;

    private bool m_stdActive = true;

    public void Start()
    {
        gem.onHit += ChangeActive;
    }

    public void ChangeActive()
    {
        if (m_stdActive == true)
        {
            m_waterStream.SetActive(true);
            m_optionalPath.isActivated = true;
            m_standardPath.isActivated = false;
            m_stdActive = false;
        }
        else if (m_stdActive == false)
        {
            m_waterStream.SetActive(false);
            m_optionalPath.isActivated = false;
            m_standardPath.isActivated = true;
            m_stdActive = true;
        }
    }
}
