using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Characters;

namespace RPG.Combat
{
	[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
	public class Weapon : ScriptableObject
	{
		[SerializeField] WeaponPrefab weaponPrefab = null;
		[SerializeField] AnimatorOverrideController animatorOverride = null;

		public void Spawn(Transform handTransform, CharacterAnimatorHandler animatorHandler)
		{
			Instantiate(weaponPrefab, handTransform);
			animatorHandler.AnimatorControllerSwicher(animatorOverride);
		}
	}
}