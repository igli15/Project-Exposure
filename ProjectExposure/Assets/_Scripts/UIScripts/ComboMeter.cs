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
		HideAllElements();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			IncreaseFill(40);
		}
		
		if (Input.GetKeyDown(KeyCode.F))
		{
			DecreaseFill(40);
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
		
		if (fillDegrees >= 360 && m_multiplier <= 9)
		{
			IncreaseMultiplier();
		}
		
	}
	
	public void DecreaseFill(float degrees)
	{
		ShowElements();
		
		m_image.DOFillAmount(m_image.fillAmount - (degrees / 360.0f), m_fillDuration);
		
		float fillDegrees = (m_image.fillAmount) * 360 - degrees;
		
		if (fillDegrees <= 0)
		{
			if (m_multiplier == 1)
			{
				HideAllElements();
			}
			else
			{
				DecreaseMultiplier();
			}
		}
	}
	
	public void DecreaseFillImmediate(float degrees)
	{
		ShowElements();

		m_image.fillAmount -=  (degrees / 360.0f);
		
		float fillDegrees = (m_image.fillAmount) * 360 - degrees;
		
		if (fillDegrees <= 0 )
		{
			Debug.Log(m_multiplier);
			if (m_multiplier == 1)
			{
				HideAllElements();
			}
			else
			{
				DecreaseMultiplier();
			}
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
		//Reset();
		m_image.fillAmount = 0;
		m_multiplier += 1;
		int index = m_multiplier - 1;
		m_fillFrames[index].gameObject.SetActive(true);
		m_multiplierText.transform.localScale = Vector3.one;
		m_multiplierText.text = "x"+m_multiplier;
		m_multiplierText.transform.DOPunchScale(Random.insideUnitCircle * m_textPunchRadius, m_textPunchDuration);
	}
	
	private void DecreaseMultiplier()
	{
		//Reset();
		
		m_image.fillAmount = 1;
		
		m_fillFrames[m_multiplier -1].gameObject.SetActive(false);
		m_multiplier -= 1;

		m_multiplierText.transform.localScale = Vector3.one;
		m_multiplierText.text = "x"+m_multiplier;
		m_multiplierText.transform.DOPunchScale(Random.insideUnitCircle * m_textPunchRadius, m_textPunchDuration);
	}


	private void Reset()
	{
		//DOTween.KillAll();
		
		m_fillFrames[0].gameObject.SetActive(true);

		m_image.fillAmount = 0;
		m_multiplierText.text = "x1";
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
		m_fillFrames[0].gameObject.SetActive(true);
	}
	
}
