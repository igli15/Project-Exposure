using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Portal : MonoBehaviour
{
	[SerializeField] private LevelLoader m_levelLoader;
    [SerializeField] private Image m_Panel;
    [SerializeField] private int m_levelToLoad = 0;
    private void Start()
    {
        m_levelLoader.OnLoadStarted.AddListener(delegate { m_Panel.DOFade(1, 0.3f); });
    }
    private int m_fuckThis = 1;

    private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Guns"))
		{
            
            if (m_fuckThis == 0)
            {
                ScoreStats.instance.UpdateHighScore();
                m_levelLoader.LoadLevel(m_levelToLoad);
            }
            m_fuckThis -= 1;
        }
	}
}
