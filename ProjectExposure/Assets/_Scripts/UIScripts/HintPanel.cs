using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Hint
{
    public string name;
    public Image image;
}

public class HintPanel : TouchButton
{
    [SerializeField] [Range(0, 1)] private float m_timeInSeconds = 0.3f;
    
    [SerializeField] private CompanionButton m_companionButton;
    [SerializeField] private Image m_backdrop;
    [SerializeField] private Image m_arrowBackdrop;
    [SerializeField] private Image m_talkBox;

    [SerializeField] private Hint[] m_hints;
    
    private Image m_borders;

    private bool m_shown;

    private Dictionary<string, Hint> m_hintDictionary;

    private string m_hintName;

    private bool m_canHide = false;

    public string hintName
    {
        get { return m_hintName; }
        set { m_hintName = value; }
    }

    public bool canHide
    {
        get { return m_canHide; }
        set { m_canHide = value; }
    }

    private void Awake()
    {
        m_hintDictionary = new Dictionary<string, Hint>();

        foreach (var h in m_hints)
        {
            m_hintDictionary.Add(h.name,h);
        }
        m_borders = GetComponent<Image>();

        m_backdrop.fillAmount = 0;
        m_arrowBackdrop.fillAmount = 0;
        m_talkBox.fillAmount = 0;
        m_borders.fillAmount = 0;
        
        gameObject.SetActive(false);
        
    }
    
    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0) && m_shown && m_canHide)
        {
            Hide();
        }
    }

    public void Show()
    {
        m_shown = false;
        m_canHide = true;
        Sequence s = DOTween.Sequence();   
        m_companionButton.gameObject.SetActive(false);
        s.Append(m_borders.DOFillAmount(1, m_timeInSeconds));
        s.Append(m_backdrop.DOFillAmount(1, m_timeInSeconds));
        s.Append(m_arrowBackdrop.DOFillAmount(1, m_timeInSeconds));
        s.Append(m_talkBox.DOFillAmount(1, m_timeInSeconds));
        s.onComplete += delegate {
            m_hintDictionary[m_hintName].image.gameObject.SetActive(true);
        };
        s.onComplete += delegate { m_shown = true; };
        
    }
	
    public void Hide()
    {
        foreach (var hint in m_hints)
        {
            hint.image.gameObject.SetActive(false);
        }
        
        Sequence s = DOTween.Sequence();

        s.Append(m_talkBox.DOFillAmount(0, m_timeInSeconds));
        s.Append(m_arrowBackdrop.DOFillAmount(0, m_timeInSeconds));
        s.Append(m_backdrop.DOFillAmount(0, m_timeInSeconds));
        s.Append(m_borders.DOFillAmount(0, m_timeInSeconds));
        
        s.onComplete+=delegate { m_companionButton.gameObject.SetActive(true); };
        s.onComplete+=delegate { m_companionButton.UnFillButton(true); };
        s.onComplete+=delegate { gameObject.SetActive(false); };
    }
}
