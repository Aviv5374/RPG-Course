using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;

        Mover mover;
        Transform target;

        bool IsInRange { get { return Vector3.Distance(transform.position, target.position) < weaponRange; } }

        void Start()
        {
            mover = GetComponent<Mover>();
        }

        // Update is called once per frame
        void Update()
        {
            // if (target == null) return;?????

            if (target)
            {              
                if (!IsInRange)
                {
                    mover.MoveTo(target.position);
                }
                else
                {
                    mover.StopMoving();
                }
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            //Debug.Log("Take that you short, squat peasant!");
            target = combatTarget.transform;
        }

        public void CancelAttack()
        {            
            target = null;
        }
    }
}
