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

    private void OnDestroy()
    {
        m_waterStream.SetActive(true);
        m_optionalPath.isActivated = true;
        m_standardPath.isActivated = true;
    }
}
