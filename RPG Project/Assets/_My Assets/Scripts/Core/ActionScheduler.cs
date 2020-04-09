using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
	public class ActionScheduler : MonoBehaviour
	{
		IAction currentAction;
										
		public void StartAction(IAction newAction)
		{
			if (currentAction == newAction) return;
			if (currentAction != null)
			{
				Debug.Log("Cancelling" + currentAction + " of " + name);
				currentAction.CancelAction();
			}
			currentAction = newAction;
		}

		public void CancelCurrentAction()
		{
			StartAction(null);
		}

	}
}