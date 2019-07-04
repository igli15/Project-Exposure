using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergedHudState : AbstractState<HudManager>
{
	
	
	public override void Enter(IAgent pAgent)
	{
		target.DisableMergeButton();

		base.Enter(pAgent);
	}

	public override void Exit(IAgent pAgent)
	{
		base.Exit(pAgent);
	}
	
}
