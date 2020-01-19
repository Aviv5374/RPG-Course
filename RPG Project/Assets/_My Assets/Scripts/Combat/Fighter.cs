﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Characters;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        Mover mover;
        CharacterAnimatorHandler myAnimator;
        ActionScheduler actionScheduler;
        Transform target;
        Health targetHealth;

        float timeSinceLastAttack = 0;

        bool IsInRange { get { return Vector3.Distance(transform.position, target.position) < weaponRange; } }

        void Start()
        {
            mover = GetComponent<Mover>();
            myAnimator = GetComponent<CharacterAnimatorHandler>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            // if (target == null) return;?????

            if (target)
            {
                if (targetHealth.HealthPoints <= 0) return;

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
            transform.LookAt(target);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                myAnimator.TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        // Animation Event
        void Hit()
        {
            if (targetHealth)
            {
                targetHealth.TakeDamage(weaponDamage);
            }            
        }

        public void Attack(CombatTarget combatTarget)
        {
            //Debug.Log("Take that you short, squat peasant!");
            //actionScheduler.StartAction(this);
            target = combatTarget.transform;
            targetHealth = combatTarget.GetComponent<Health>();
        }

        public void CancelAttack()
        {
            myAnimator.TriggerStopAttack();
            target = null;
            targetHealth = null;
        }

        public void CancelAction()
        {            
            CancelAttack();
        }

    }
}
