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
		
		//m_rightGun.OnHueChanged += MixColorOfGuns;
		//m_leftGun.OnHueChanged += MixColorOfGuns;
		
		m_rightGun.OnColorChanged += MixColorOfGuns;
		m_leftGun.OnColorChanged += MixColorOfGuns;


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

		//mixture = Color.cyan + Color.magenta;

		mixture = c1 + c2;
		return mixture;

		/*
		Color color1 = c1;
		Color color2 = c2;

		float d1 = 0;
		if (color1.r > 0) d1 += 1;
		if (color1.g > 0) d1 += 1;
		if (color1.b > 0) d1 += 1;

		color1 /= d1;
		
		float d2 = 0;
		if (color2.r > 0) d2 += 1;
		if (color2.g > 0) d2 += 1;
		if (color2.b > 0) d2 += 1;

		color2 /= d2;
		mixture = color1 + color2;
		return mixture;
		*/

	}

	public GameObject mergeSphere
	{
		get { return m_mergeSphere; }
	}
}
