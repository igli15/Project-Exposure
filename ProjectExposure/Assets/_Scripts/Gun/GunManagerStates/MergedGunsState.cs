using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class MergedGunsState : GunState
{
	public enum GunMode
	{
		COLOR,
		SHOOT,
	}

	[SerializeField] private Gun m_colorGun;
	[SerializeField] private Gun m_damageGun;

	[SerializeField] private LineRenderer m_lineRenderer;
	[SerializeField] private Material m_beamMat;
	
	private GunMode m_currentMode;
	

	public GunMode currentMode
	{
		get { return m_currentMode; }
	}

	public override void Enter(IAgent pAgent)
	{
		base.Enter(pAgent);
	}

	public override void Exit(IAgent pAgent)
	{
		base.Exit(pAgent);
	}

	public override void Shoot()
	{
		List<Hittable> hittables = target.RaycastFromGuns();


		foreach (Hittable h in hittables)
		{
			Color c = m_beamMat.GetColor("_TintColor");
			c.a = 1;
			m_beamMat.SetColor("_TintColor", c);
			
			m_lineRenderer.SetPosition(0,m_lineRenderer.transform.position);
			m_lineRenderer.SetPosition(1,hittables[0].transform.position);
			

			c = m_beamMat.GetColor("_TintColor");
			DOVirtual.Float(c.a, 0, 0.5f, 
				(delegate(float value) { c.a = value;
					m_lineRenderer.SetPosition(0,m_lineRenderer.transform.position);
				m_beamMat.SetColor("_TintColor", c);
			}));
			
			
			if (h.GetColor() == Color.white)
			{
				m_currentMode = GunMode.COLOR;
				m_colorGun.Shoot();
				h.Hit(target,0,target.color);
					
			}
			else
			{
				m_currentMode = GunMode.SHOOT;
				m_damageGun.Shoot();
				float m_damage = target.CalculateDamage(target.color, h.GetColor());
				h.Hit(target,m_damage,target.color);
			}
		}
	}
}
