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

	
	public enum GunMode
	{
		COLOR,
		SHOOT
	}

	[FormerlySerializedAs("m_leftGun")] [SerializeField] private SingleGun leftSingleGun;
	[FormerlySerializedAs("m_rightGun")] [SerializeField] private SingleGun rightSingleGun;
	
	private SingleGun m_currentSingleGun;
	private GunMode m_currentMode;

	public GunMode currentMode
	{
		get { return m_currentMode; }
	}

	public override void Enter(IAgent pAgent)
	{
		if (OnSplit != null) OnSplit(this);
		
		leftSingleGun.manager = target;
		rightSingleGun.manager = target;
		
		base.Enter(pAgent);
	}

	public override void Exit(IAgent pAgent)
	{
		base.Exit(pAgent);
	}

	public override void Shoot()
	{
		if (target.isMouseDown || EventSystem.current.IsPointerOverGameObject()) return; //if the mouse is clicking on the gun dont shoot!
		
		//Hittable hittable = target.RaycastFromGuns();

		Vector3 cameraViewportPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
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
}
