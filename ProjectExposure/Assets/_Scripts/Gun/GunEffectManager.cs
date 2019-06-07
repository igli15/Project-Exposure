using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class GunEffectManager : MonoBehaviour 
{

	[Header("Beam")]
	[SerializeField] private LineRenderer m_lineRenderer;

	[SerializeField] private Material m_rainbowBeam;
	[SerializeField] private Material m_shootBeam;

	[Range(0.1f, 2)] [SerializeField] private float m_colorIntensity = 2;
	[Range(0, 1)] [SerializeField] private float m_beamFadeDuration = 0.3f;
	[Range(0, 1)] [SerializeField] private float m_saturationScale = 0.5f;
	[Range(0, 1)] [SerializeField] private float m_valueScale = 1;

	private Material m_muzzleFlashMaterial;
	
	private Renderer m_beamRenderer;

	private Color m_muzzleColor;

	private Sequence m_muzzleFlashSequence;
	private void Awake()
	{
		m_beamRenderer = m_lineRenderer.GetComponent<Renderer>();

		SplitGunsState.OnShoot += InitSplitGunRays;
		SplitGunsState.OnShoot += PlayGunParticles;
		MergedGunsState.OnShoot += InitMergeGunRay;
		
	}


	public void PlayGunParticles(Hittable hittable, GunManager gunManager,Gun gun)
	{
		
		gun.GetEffectGroupAt(0).PlayARandomEffectInAColor(gunManager.color);
	}

	public void InitMergeGunRay(Hittable hittable, GunManager manager)
	{
		//if (manager.isMouseDown) return;

		Material mat = m_rainbowBeam;
		Color c = mat.GetColor("_TintColor");;
		c.a = 1;

		m_beamRenderer.material = mat;
		mat.SetColor("_TintColor", c);

		SetLineRendererPoints(hittable,manager,manager.origin);

		DOVirtual.Float(c.a, 0, m_beamFadeDuration,
			(delegate(float value)
			{
				c.a = value;
				m_lineRenderer.SetPosition(0, manager.origin.position);
				mat.SetColor("_TintColor", c);
			}));
	}

	public void InitSplitGunRays(Hittable hittable,GunManager manager,Gun gun)
	{
		//if (manager.isMouseDown) return;

		Color c = manager.color;

		Vector3 hsv = ColorUtils.GetHSVOfAColor(c);
		hsv.y *= m_saturationScale;
		hsv.z *= m_valueScale;
		c = Color.HSVToRGB(hsv.x, hsv.y, hsv.z, true);
		c *= m_colorIntensity;
		c.a = 1;

		Material mat = m_shootBeam;

		m_beamRenderer.material = mat;
		mat.SetColor("_TintColor", c);

		SetLineRendererPoints(hittable,manager,gun.shootTransform);

		c = mat.GetColor("_TintColor");
		DOVirtual.Float(c.a, 0, m_beamFadeDuration,
			(delegate(float value)
			{
				c.a = value;
				m_lineRenderer.SetPosition(0, gun.shootTransform.position);
				mat.SetColor("_TintColor", c);
			}));
	}
	
	private void SetLineRendererPoints(Hittable hittable,GunManager manager,Transform origin)
	{
		//m_lineRenderer.SetPosition(0,origin.position);
		
		/*
		int distribution = m_lineRenderer.positionCount;
		
		for (int i = 0; i < distribution; i++)
		{
			float lerpAmount = (float)i / distribution;
			//Debug.Log(i+ "  " + lerpAmount);
			if(hittable != null) m_lineRenderer.SetPosition(i,Vector3.Lerp(origin.position,hittable.transform.position,lerpAmount));
			else
			{
				Vector3 finalpos = origin.position + manager.GetDirFromGunToMouse() * 40;
				//Debug.Log(origin.position);
				//Debug.Log(i + "  " + Vector3.Lerp(origin.position,finalpos,lerpAmount));
				m_lineRenderer.SetPosition(i,Vector3.Lerp(origin.position,finalpos,lerpAmount));
			}
				
		}
		*/
		Vector3 finalpos = origin.position + manager.GetDirFromGunToMouse() * 20;
		m_lineRenderer.SetPosition(1,finalpos); 
		
	}
	

	private void OnDestroy()
	{
	
	}
}
