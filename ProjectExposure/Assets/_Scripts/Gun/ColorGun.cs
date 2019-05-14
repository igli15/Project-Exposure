using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorGun : AbstractGun
{
	[SerializeField] private Renderer m_beamRenderer;
	
	public override void Start()
	{
		base.Start();

		SetColor(Color.red);
	}

	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
	}

	protected override void Shoot()
	{
		if(EventSystem.current.IsPointerOverGameObject()) return;
        
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
		//if(m_fsm.GetCurrentState() is SeperatedGunState)
		//	LookInRayDirection(ray);
      
		RaycastHit[] hits;
		hits = Physics.RaycastAll(ray);

		for (int i = 0; i < hits.Length; i++)
		{
			RaycastHit hit = hits[i];
			Hittable hittable = hit.transform.gameObject.GetComponent<Hittable>();
            
			if(hittable != null)
			{
				hittable.HitByGun(this);
			}
		}
	}

	public float GetHueOfColor(Color color)
	{
		float hue = 1;
		float saturation = 0;
		float value = 0;

		Color.RGBToHSV(color, out hue, out saturation, out value);
		return hue;
	}

	public override void SetColor(Color newColor)
	{
		base.SetColor(newColor);

		m_beamRenderer.material.color = newColor;
		m_color = newColor;
		m_beamRenderer.material.SetFloat("_Wavelength",((GetHueOfColor(newColor) -2)  * -2));
	}
}
