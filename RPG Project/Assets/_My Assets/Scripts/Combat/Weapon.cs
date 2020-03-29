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
		[SerializeField] Projectile projectile = null;
		[SerializeField] float damage = 5f;
		[SerializeField] float range = 2f;
		[SerializeField] AnimatorOverrideController animatorOverride = null;
		//if is a character with 3 hand or more it need to be an enum
		//What happens if the character has less than two hands? And do you get into a minus hands?
		[SerializeField] bool isRightHanded = true;
		
		public float Damage { get => damage;}
		public float Range { get => range;}
		public bool HasProjectile { get { return projectile; } }

		public void Spawn(Transform rightHand, Transform LeftHand, CharacterAnimatorHandler animatorHandler)
		{
			if(!weaponPrefab || !animatorOverride) { return; }
			Instantiate(weaponPrefab, GetAHand(rightHand, LeftHand));
			animatorHandler.AnimatorControllerSwicher(animatorOverride);
		}

		public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
		{
			Projectile projectileInstance = Instantiate(projectile, GetAHand(rightHand, leftHand).position, Quaternion.identity);
			projectileInstance.Target = target;
		}

		Transform GetAHand(Transform rightHand, Transform LeftHand)
		{
			if (isRightHanded)
			{
				return rightHand;
			}

			return LeftHand;
		}
	}
}