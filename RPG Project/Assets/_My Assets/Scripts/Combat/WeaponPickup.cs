using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;

        //I prefer EquipWeapon() be called through the PlayerController rather than directly through Fighter
        private void OnTriggerEnter(Collider other)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player)
            {
                Fighter fighter = player.GetComponent<Fighter>();
                fighter.EquipWeapon(weapon);
                Destroy(gameObject);
            }
        }
    }
}