using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
	public class ActionScheduler : MonoBehaviour
	{
		IAction currentAction;

		void Awake()
		{

		}
		
		void Start()
		{

		}
		
		void Update()
		{

		}

		public void StartAction(IAction newAction)
		{
			if (currentAction == newAction) return;
			if (currentAction != null)
			{
				Debug.Log("Cancelling" + currentAction);
				currentAction.CancelAction();
			}
			currentAction = newAction;
		}
		
	}
}