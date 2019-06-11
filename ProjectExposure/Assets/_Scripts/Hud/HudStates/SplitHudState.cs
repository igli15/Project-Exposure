using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitHudState : AbstractState<HudManager> 
{
	public override void Enter(IAgent pAgent)
	{
		target.MoveElementsInsideCanvas();
		base.Enter(pAgent);
	}

	public override void Exit(IAgent pAgent)
	{
		base.Exit(pAgent);
	}
}
