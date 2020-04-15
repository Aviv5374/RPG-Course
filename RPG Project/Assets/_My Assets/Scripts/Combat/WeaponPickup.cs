﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.Combat
{
    public class WeaponPickup : Pickup
    {
        [SerializeField] Weapon weapon = null;        



        protected override void InvokePickup(PlayerController player)
        {
            player.Fighter.EquipWeapon(weapon);
            base.InvokePickup(player);
        }




    }
}