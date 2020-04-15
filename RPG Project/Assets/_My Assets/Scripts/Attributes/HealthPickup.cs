using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using RPG.Combat;

namespace RPG.Attributes
{
    public class HealthPickup : Pickup
    {
        [SerializeField] float healthToRestore = 0;
                
        protected override void InvokePickup(PlayerController player)
        {
            player.GetComponent<Health>().Heal(healthToRestore);
            base.InvokePickup(player);
        }
    }
}