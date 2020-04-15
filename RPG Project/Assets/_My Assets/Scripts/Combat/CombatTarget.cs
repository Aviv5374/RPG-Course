using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using RPG.Control;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

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
