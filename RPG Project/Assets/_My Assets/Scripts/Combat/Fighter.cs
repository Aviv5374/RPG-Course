using System.Collections;
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

        bool isChasing = false;
        bool isInjured = false;
        float timeSinceLastAttack = Mathf.Infinity;

        bool IsInRange { get { return Vector3.Distance(transform.position, target.position) < weaponRange; } }
        
        void Start()
        {
            mover = GetComponent<Mover>();
            myAnimator = GetComponent<CharacterAnimatorHandler>();
            actionScheduler = GetComponent<ActionScheduler>();

            isChasing = false;
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
                    mover.MoveTo(target.position, isInjured, isChasing);
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
                //TriggerAttack();???
                myAnimator.TriggerAttack();
                timeSinceLastAttack = Mathf.Infinity;
            }
        }

        void TriggerAttack()
        {
            myAnimator.ResetStopAttackTrigger();
            myAnimator.TriggerAttack();
        }

        // Animation Event
        void Hit()
        {
            if (targetHealth)
            {
                targetHealth.TakeDamage(weaponDamage);
            }            
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if (!combatTarget) { return false; }
            Health targetHealth = combatTarget.GetComponent<Health>();
            string targetName = combatTarget.name;            
            return targetHealth && targetHealth.HealthPoints > 0/*OR targetHealth.IsAlive?????*/ && targetName != this.name;
        }

        //Right now only enemies are chasing the player, what if something else is chasing her?        
        public void Attack(CombatTarget combatTarget, bool isInjured = false, bool isChasing = false)
        {
            //Debug.Log("Take that you short, squat peasant!");
            //actionScheduler.StartAction(this);
            target = combatTarget.transform;
            targetHealth = combatTarget.GetComponent<Health>();
            myAnimator.ResetStopAttackTrigger();
            this.isInjured = isInjured;
            this.isChasing = isChasing;
        }

        public void CancelAttack()
        {
            StopAttack();
            timeSinceLastAttack = Mathf.Infinity;
            ResetTarget();
            //mover.StopMoving();?????
        }

        void StopAttack()
        {
            myAnimator.ResetAttackTrigger();
            myAnimator.TriggerStopAttack();
        }

        void ResetTarget()
        {
            target = null;
            targetHealth = null;
        }

        public void CancelAction()
        {            
            CancelAttack();
        }

    }
}
