using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : WorldButton
{
	[SerializeField] private LevelLoader m_levelLoader;

	private void Start()
	{
		
	}

	public override void Click(Ray ray)
	{
		base.Click(ray);

		if (enabled)
		{
			PlayerStats.instance.ResetAllData();
			m_levelLoader.LoadLevel(5);
		}

	}
}
