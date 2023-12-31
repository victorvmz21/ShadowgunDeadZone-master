//
// By using or accessing the source codes or any other information of the Game SHADOWGUN: DeadZone ("Game"),
// you ("You" or "Licensee") agree to be bound by all the terms and conditions of SHADOWGUN: DeadZone Public
// License Agreement (the "PLA") starting the day you access the "Game" under the Terms of the "PLA".
//
// You can review the most current version of the "PLA" at any time at: http://madfingergames.com/pla/deadzone
//
// If you don't agree to all the terms and conditions of the "PLA", you shouldn't, and aren't permitted
// to use or access the source codes or any other information of the "Game" supplied by MADFINGER Games, a.s.
//

using System;
using UnityEngine;

/******************************************************************
 * GOAL STATS
 * 
 * For initialize : 
 *                  E_PropKey.E_IDLING              true
 * For planing : 
 *                  E_PropKey.E_IDLING              false
 * Set : 
 *                  
 * Finished:
 *                  When action is done
 * 
 * ***************************************************************/

class GOAPGoalWeaponReload : GOAPGoal
{
	public GOAPGoalWeaponReload(AgentHuman owner) : base(E_GOAPGoals.WeaponReload, owner)
	{
	}

	public override void InitGoal()
	{
	}

	public override float GetMaxRelevancy()
	{
		return Owner.BlackBoard.GoapSetup.ReloadRelevancy;
	}

	public override void CalculateGoalRelevancy()
	{
		GoalRelevancy = 0;

		WorldStateProp prop = Owner.WorldState.GetWSProperty(E_PropKey.WeaponLoaded);

		if (prop == null || prop.GetBool() == true)
			return;

		if (Owner.WeaponComponent.GetCurrentWeapon().WeaponAmmo == 0 || Owner.WeaponComponent.GetCurrentWeapon().IsBusy())
			return;

		GoalRelevancy = Owner.BlackBoard.GoapSetup.ReloadRelevancy;
	}

	public override void SetDisableTime()
	{
		NextEvaluationTime = Owner.BlackBoard.GoapSetup.ReloadDelay + Time.timeSinceLevelLoad;
	}

	public override void SetWSSatisfactionForPlanning(WorldState worldState)
	{
		worldState.SetWSProperty(E_PropKey.WeaponLoaded, true);
	}

	public override bool IsWSSatisfiedForPlanning(WorldState worldState)
	{
		WorldStateProp prop = worldState.GetWSProperty(E_PropKey.WeaponLoaded);

		return prop.GetBool();
	}

	public override bool IsSatisfied()
	{
		return IsPlanFinished();
	}
}
