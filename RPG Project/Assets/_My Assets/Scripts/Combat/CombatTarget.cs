using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Resources;
using RPG.Control;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public bool HandleRaycast(PlayerController callingController)
        {
            if (!callingController.Fighter.CanAttack(this))
            {
                return false;
            }


            if (Input.GetMouseButton(0))
            {
                callingController.Fighter.Attack(this);
            }

            return true;
        }
    }
}
