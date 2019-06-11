using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

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

	private GunManager m_GunManager;
	private void Awake()
	{
		m_beamRenderer = m_lineRenderer.GetComponent<Renderer>();

		SplitGunsState.OnShoot += InitSplitGunRays;
		SplitGunsState.OnShoot += PlayGunParticles;
		MergedGunsState.OnShoot += InitMergeGunRay;
		MergedGunsState.OnShoot += PlayMergeShotParticles;

	}


	public void PlayGunParticles(SingleGun singleGun,Hittable hittable)
	{
		if (EventSystem.current.IsPointerOverGameObject()) return ;

		singleGun.GetEffectGroupAt(0).PlayARandomEffectInAColor(singleGun.color);
		singleGun.GetEffectGroupAt(1).PlayAllEffects();
	}

	public void PlayMergeShotParticles(MergedGun mergedGun, Hittable hittable)
	{
		if (EventSystem.current.IsPointerOverGameObject()) return ;
		mergedGun.GetEffectGroupAt(0).PlayAllEffects();
	}

	public void InitMergeGunRay(MergedGun gun,Hittable hittable)
	{
		if (EventSystem.current.IsPointerOverGameObject()) return ;

		Material mat = m_rainbowBeam;
		Color c = mat.GetColor("_TintColor");;
		c.a = 1;

		m_beamRenderer.material = mat;
		mat.SetColor("_TintColor", c);

		SetLineRendererPoints(gun);

		DOVirtual.Float(c.a, 0, m_beamFadeDuration,
			(delegate(float value)
			{
				c.a = value;
				m_lineRenderer.SetPosition(0, gun.origin.position);
				mat.SetColor("_TintColor", c);
			}));
	}

	public void InitSplitGunRays(SingleGun gun,Hittable hittable)
	{
		if (EventSystem.current.IsPointerOverGameObject()) return ;

		Color c = gun.color;

		Vector3 hsv = ColorUtils.GetHSVOfAColor(c);
		hsv.y *= m_saturationScale;
		hsv.z *= m_valueScale;
		c = Color.HSVToRGB(hsv.x, hsv.y, hsv.z, true);
		c *= m_colorIntensity;
		c.a = 1;

		Material mat = m_shootBeam;

		m_beamRenderer.material = mat;
		mat.SetColor("_TintColor", c);

		SetLineRendererPoints(gun);

		c = mat.GetColor("_TintColor");
		DOVirtual.Float(c.a, 0, m_beamFadeDuration,
			(delegate(float value)
			{
				c.a = value;
				m_lineRenderer.SetPosition(0, gun.origin.position);
				mat.SetColor("_TintColor", c);
			}));
	}
	
	private void SetLineRendererPoints(AbstractGun gun)
	{
		
		Vector3 finalpos = gun.origin.position + gun.GetDirFromGunToMouse() * 20;
		m_lineRenderer.SetPosition(1,finalpos); 
		
	}
	

	private void OnDestroy()
	{
		SplitGunsState.OnShoot -= InitSplitGunRays;
		SplitGunsState.OnShoot -= PlayGunParticles;
		MergedGunsState.OnShoot -= InitMergeGunRay;
	}
}
