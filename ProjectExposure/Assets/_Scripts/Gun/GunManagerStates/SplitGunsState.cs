using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class SplitGunsState : GunState
{

	public static Action<SingleGun,Hittable> OnShoot;
	public static Action<SplitGunsState> OnSplit;
	public static Action<SplitGunsState> OnColorsCollected;

	public enum GunMode
	{
		COLOR,
		SHOOT
	}

	[FormerlySerializedAs("m_leftGun")] [SerializeField] private SingleGun leftSingleGun;
	[FormerlySerializedAs("m_rightGun")] [SerializeField] private SingleGun rightSingleGun;
	[SerializeField] private HudCrystalManager m_hudCrystalManager;
	
	private List<Color> m_collectedColors ;
	private SingleGun m_currentSingleGun;
	private GunMode m_currentMode;

	private bool m_collectedAllCrystals;
	public GunMode currentMode
	{
		get { return m_currentMode; }
	}

	public override void Enter(IAgent pAgent)
	{
		if (OnSplit != null) OnSplit(this);
		
		leftSingleGun.manager = target;
		rightSingleGun.manager = target;
		
		m_collectedColors.Clear();
		m_collectedAllCrystals = false;
		
		for (int i = 0; i < 7; i++)
		{
			m_collectedColors.Add(Color.clear);
		}
		
		base.Enter(pAgent);
	}
	
	private void Awake()
	{
		m_collectedColors = new List<Color>();
		
	}

	public override void Exit(IAgent pAgent)
	{
		base.Exit(pAgent);

		m_hudCrystalManager.StartFadingCrystals();

		
	}

	public override void Shoot(int touchIndex)
	{
		//if (target.isMouseDown || EventSystem.current.IsPointerOverGameObject()) return; //if the mouse is clicking on the gun dont shoot!

        //Hittable hittable = target.RaycastFromGuns();
        //Debug.Log(touchIndex);
		Vector3 cameraViewportPos = Camera.main.ScreenToViewportPoint(Input.GetTouch(touchIndex).position);
        //Debug.Log(cameraViewportPos);

		if (cameraViewportPos.x > 0.5f) m_currentSingleGun = rightSingleGun;
		else m_currentSingleGun = leftSingleGun;
		
    
		Hittable hittable = m_currentSingleGun.Shoot(touchIndex);

		if(OnShoot != null) OnShoot(m_currentSingleGun,hittable);
		
	}

	public override void Shoot()
	{
		if (target.isMouseDown || EventSystem.current.IsPointerOverGameObject()) return; //if the mouse is clicking on the gun dont shoot!

		//Hittable hittable = target.RaycastFromGuns();
		//Debug.Log(touchIndex);
		Vector3 cameraViewportPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		//Debug.Log(cameraViewportPos);

		if (cameraViewportPos.x > 0.5f) m_currentSingleGun = rightSingleGun;
		else m_currentSingleGun = leftSingleGun;
		
    
		Hittable hittable = m_currentSingleGun.Shoot();

		if(OnShoot != null) OnShoot(m_currentSingleGun,hittable);
	}

	public override void SetGunColor(Color c)
	{
		leftSingleGun.color = c;
		rightSingleGun.color = c;
	}

	public void AddCollectedColor(Color c)
	{
		if (m_collectedAllCrystals) return;
		
		if (ColorUtils.CheckIfColorAreSimilar(c , m_hudCrystalManager.GetCrystalAt(0).color,15))
		{
			m_collectedColors[0]=  m_hudCrystalManager.GetCrystalAt(0).color;
			m_hudCrystalManager.ActivateCrystalAt(0);
		}
		else if (ColorUtils.CheckIfColorAreSimilar(c , m_hudCrystalManager.GetCrystalAt(1).color,15) )
		{
			m_collectedColors[1]=  m_hudCrystalManager.GetCrystalAt(1).color;
			m_hudCrystalManager.ActivateCrystalAt(1);
		}
		else if (ColorUtils.CheckIfColorAreSimilar(c , m_hudCrystalManager.GetCrystalAt(2).color,15))
		{
			m_collectedColors[2]=  m_hudCrystalManager.GetCrystalAt(2).color;
			m_hudCrystalManager.ActivateCrystalAt(2);
		}
		else if (ColorUtils.CheckIfColorAreSimilar(c , m_hudCrystalManager.GetCrystalAt(3).color,15))
		{
			m_collectedColors[3]=  m_hudCrystalManager.GetCrystalAt(3).color;
			m_hudCrystalManager.ActivateCrystalAt(3);
		}
		else if (ColorUtils.CheckIfColorAreSimilar(c , m_hudCrystalManager.GetCrystalAt(4).color,15))
		{
			m_collectedColors[4]=  m_hudCrystalManager.GetCrystalAt(4).color;
			m_hudCrystalManager.ActivateCrystalAt(4);
		}
		else if (ColorUtils.CheckIfColorAreSimilar(c , m_hudCrystalManager.GetCrystalAt(5).color,15))
		{
			m_collectedColors[5]=  m_hudCrystalManager.GetCrystalAt(5).color;
			m_hudCrystalManager.ActivateCrystalAt(5);
		}
		else if (ColorUtils.CheckIfColorAreSimilar(c , m_hudCrystalManager.GetCrystalAt(6).color,15))
		{
			m_collectedColors[6]=  m_hudCrystalManager.GetCrystalAt(6).color;
			m_hudCrystalManager.ActivateCrystalAt(6);
		}

		if (!m_collectedAllCrystals &&  CheckIfCompletedList())
		{
			m_collectedAllCrystals = true;
			if(OnColorsCollected != null) OnColorsCollected(this);
		}
	}

	private bool CheckIfCompletedList()
	{
		foreach (Color c in m_collectedColors)
		{
			if (c == Color.clear) return false;
		}
		return true;
	}
}
