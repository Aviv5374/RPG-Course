using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Characters;

namespace RPG.Combat
{
	[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
	public class Weapon : ScriptableObject
	{
		[SerializeField] WeaponPrefab weaponPrefab = null;//OR [SerializeField] GameObject equippedPrefab = null;
		[SerializeField] float damage = 5f;
		[SerializeField] float range = 2f;
		[SerializeField] AnimatorOverrideController animatorOverride = null;

		public float Damage { get => damage;}
		public float Range { get => range;}

		public void Spawn(Transform handTransform, CharacterAnimatorHandler animatorHandler)
		{
			if(!weaponPrefab || !animatorOverride) { return; }
			Instantiate(weaponPrefab, handTransform);
			animatorHandler.AnimatorControllerSwicher(animatorOverride);
		}
	}
}