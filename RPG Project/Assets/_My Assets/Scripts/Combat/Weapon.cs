﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Characters;
using RPG.Attributes;

namespace RPG.Combat
{
	[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
	public class Weapon : ScriptableObject
	{
		const string weaponName = "Weapon";

		[SerializeField] WeaponPrefab weaponPrefab = null;
		[SerializeField] Projectile projectile = null;
		[SerializeField] float damage = 5f;
		[SerializeField] float range = 2f;
		[Header("???????")]
		[SerializeField] float percentageBonus = 0;//???????
		[SerializeField] AnimatorOverrideController animatorOverride = null;
		//if is a character with 3 hand or more it need to be an enum
		//What happens if the character has less than two hands? And do you get into a minus hands?
		[SerializeField] bool isRightHanded = true;

		public WeaponPrefab WeaponPrefab{ get { return weaponPrefab; } }
		public float Damage { get => damage;}
		public float Range { get => range;}
		public float PercentageBonus { get => percentageBonus;}
		public bool HasProjectile { get { return projectile; } }


		public void Spawn(Transform rightHand, Transform leftHand, CharacterAnimatorHandler animatorHandler)
		{
			DestroyOldWeapon(rightHand, leftHand);

			if (weaponPrefab) 
			{
				WeaponPrefab weapon = Instantiate(weaponPrefab, GetAHand(rightHand, leftHand));
				weapon.name = weaponName;
			}

			animatorHandler.AnimatorControllerSwicher(animatorOverride);	
		}

		void DestroyOldWeapon(Transform rightHand, Transform leftHand)
		{
			Transform oldWeapon = rightHand.Find(weaponName);
			if (!oldWeapon)
			{
				oldWeapon = leftHand.Find(weaponName);
			}
			if (!oldWeapon) { return; }

			//Name Change is for Fixig search and frame issues and makes sure the right weapons are destroyed.
			oldWeapon.name = "DESTROYING";
			Destroy(oldWeapon.gameObject);
		}

		public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
		{
			Projectile projectileInstance = Instantiate(projectile, GetAHand(rightHand, leftHand).position, Quaternion.identity);
			projectileInstance.Target = target;
			projectileInstance.Damage += calculatedDamage;
			projectileInstance.Instigator = instigator;
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