using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{

	[SerializeField] private Gun m_leftGun;
	[SerializeField] private Gun m_rightGun;

	[SerializeField] private GameObject m_mergeSphere;

	private Renderer m_leftGunRenderer;
	private Renderer m_rightGunRenderer;

	private Renderer m_mergeSphereRenderer;
	
	// Use this for initialization
	void Start ()
	{
		m_leftGun.manager = this;
		m_rightGun.manager = this;
		
		m_rightGun.OnHueChanged += MixColorOfGuns;
		m_leftGun.OnHueChanged += MixColorOfGuns;

		m_leftGunRenderer = m_leftGun.GetComponent<Renderer>();
		m_rightGunRenderer = m_rightGun.GetComponent<Renderer>();
		m_mergeSphereRenderer = m_mergeSphere.GetComponent<Renderer>();
	}

	public void MixColorOfGuns(Gun gun)
	{

		Color mixedColor = MixColors(m_leftGunRenderer.material.GetColor("_Color"),
			m_rightGunRenderer.material.GetColor("_Color"));
		
		m_mergeSphereRenderer.material.SetColor("_Color",mixedColor);
	}
	Color MixColors(Color c1, Color c2)
	{
		Color mixture = Color.black;
		
		
		
			
		mixture.r = (c1.r + c2.r) / 2;
		mixture.g = (c1.g + c2.g) / 2;
		mixture.b = (c1.b + c2.b) / 2;
		
			
			mixture = Color.Lerp(c1,c2,0.5f);
		return mixture;
	}

	public GameObject mergeSphere
	{
		get { return m_mergeSphere; }
	}
}
