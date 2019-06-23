using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ComboMeter : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI m_multiplierText;

	[SerializeField] private Image m_brokenImage;

	[SerializeField] private Image m_outlineImage;
	
	[SerializeField] private Image[] m_fillFrames;

	[Space] 
	[Header("Tween Values")] 
	[SerializeField] [Range(0, 1)] private float m_fillDuration = 0.3f;
	[SerializeField] [Range(0, 1)] private float m_breakFadeDuration = 0.2f;
	[SerializeField] [Range(0, 1)] private float m_textPunchDuration = 0.2f;
	[SerializeField] [Range(0, 1)] private float m_breakPunchDuration = 0.2f;
	
	[SerializeField] [Range(0, 3)] private float m_breakPunchRadius = 0.5f;
	[SerializeField] [Range(0, 3)] private float m_textPunchRadius = 2;

	private Image m_image;

	private int m_multiplier = 1;

	public int multiplier
	{
		get { return m_multiplier; }
	}

	// Use this for initialization
	void Start ()
	{
		m_image = GetComponent<Image>();
		Reset();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			IncreaseFill(40);
		}
		
		if (Input.GetKeyDown(KeyCode.B))
		{
			BreakCombo();
		}
	}


	public void IncreaseFill(float degrees)
	{
		ShowElements();
		
		m_image.DOFillAmount(m_image.fillAmount + (degrees / 360.0f), m_fillDuration);
		float fillDegrees = (m_image.fillAmount) * 360 + degrees;

		int index = (int)(fillDegrees / 36.0f) - 1;
		
		if(index >= 0 && index <= 9) 
			m_fillFrames[index].gameObject.SetActive(true);
		
		
		if (index >= 9 && m_multiplier < 9)
		{
			IncreaseMultiplier();
		}
		

	}

	public void BreakCombo()
	{
		Reset();
		HideAllElements();
		
		m_brokenImage.DOFade(1,0);
		m_brokenImage.DOFade(0, m_breakFadeDuration);
		
		m_brokenImage.transform.localScale = Vector3.one;
		m_brokenImage.transform.DOPunchScale(Random.insideUnitCircle * m_breakPunchRadius,m_breakPunchDuration);
		m_multiplier = 1;
	}
	
	private void IncreaseMultiplier()
	{
		Reset();
		m_multiplier += 1;
		m_multiplierText.text = m_multiplier + "X";
		m_multiplierText.transform.DOPunchScale(Random.insideUnitCircle * m_textPunchRadius, m_textPunchDuration);
	}


	private void Reset()
	{
		DOTween.KillAll();
		for (int i = 0; i < m_fillFrames.Length; i++)
		{
			m_fillFrames[i].gameObject.SetActive(false);
		}

		m_image.fillAmount = 0;
		m_multiplierText.text = "1X";
	}

	private void HideAllElements()
	{
		foreach (Image i in m_fillFrames)
		{
			i.gameObject.SetActive(false);
		}

		m_image.fillAmount = 0;
		m_outlineImage.gameObject.SetActive(false);
		m_multiplierText.gameObject.SetActive(false);
	}

	private void ShowElements()
	{
		m_outlineImage.gameObject.SetActive(true);
		m_multiplierText.gameObject.SetActive(true);
	}
	
}
