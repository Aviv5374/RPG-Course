using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Characters.Player;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;

        Mover mover;
        PlayerAnimatorHandler myAnimator;
        ActionScheduler actionScheduler;
        Transform target;

        float timeSinceLastAttack = 0;

        bool IsInRange { get { return Vector3.Distance(transform.position, target.position) < weaponRange; } }

        void Start()
        {
            mover = GetComponent<Mover>();
            myAnimator = GetComponent<PlayerAnimatorHandler>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            // if (target == null) return;?????

            if (target)
            {
                if (!IsInRange)
                {
                    mover.MoveTo(target.position);
                }
                else
                {
                    mover.CancelAction();//OR mover.StopMoving();
                    AttackBehaviour();
                }
            }
        }

        void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                myAnimator.TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            //Debug.Log("Take that you short, squat peasant!");
            //actionScheduler.StartAction(this);
            target = combatTarget.transform;
        }

        public void CancelAttack()
        {            
            target = null;
        }

        public void CancelAction()
        {            
            CancelAttack();
        }

        // Animation Event
        void Hit()
        {

        }

    }
}
