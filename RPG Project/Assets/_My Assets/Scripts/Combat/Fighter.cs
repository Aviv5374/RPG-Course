using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Characters;
using RPG.My.Saving;
using RPG.Attributes;
using RPG.Stats;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, IMySaveable, IModifierProvider
    {        
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;        

        Mover mover;
        CharacterAnimatorHandler myAnimator;
        ActionScheduler actionScheduler;
        BaseStats myBaseStats;
        Transform target;
        Health targetHealth;

        bool isChasing = false;
        bool isInjured = false;
        float timeSinceLastAttack = Mathf.Infinity;
        Weapon currentWeapon = null;

        bool IsInRange { get { return Vector3.Distance(transform.position, target.position) < currentWeapon.Range; } }
        public Health TargetHealth { get => targetHealth; }

        void Awake()
        {
            mover = GetComponent<Mover>();
            myAnimator = GetComponent<CharacterAnimatorHandler>();
            actionScheduler = GetComponent<ActionScheduler>();
            myBaseStats = GetComponent<BaseStats>();
        }

        void Start()
        {            
            isChasing = false;
            if (!currentWeapon)
            {
                EquipWeapon(defaultWeapon);
            }
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            // if (target == null) return;?????

            if (target)
            {
                if (TargetHealth.HealthPoints <= 0) return;

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

        void SetupWeapon(string weaponName)
        {
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }

        public void EquipWeapon(Weapon weapon)
        {            
            currentWeapon = weapon;           
            currentWeapon.Spawn(rightHandTransform, leftHandTransform, myAnimator);
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

        #region Animation Events
        void Hit()
        {
            if (TargetHealth)
            {
                float damage = myBaseStats.GetStat(Stat.Damage);

                if (currentWeapon.WeaponPrefab)
                {
                    currentWeapon.WeaponPrefab.OnHit();
                }
                if (currentWeapon.HasProjectile)
                {
                    currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, TargetHealth, gameObject, damage);
                }
                else 
                {
                    TargetHealth.TakeDamage(gameObject, damage);
                }
            }            
        }

        void Shoot()
        {
            Hit();
        }

        #endregion

        public bool CanAttack(CombatTarget combatTarget)
        {
            if (!combatTarget || combatTarget.gameObject == this.gameObject) { return false; }
            if (!mover.CanMoveTo(combatTarget.transform.position) && !IsInRange) 
            {
                ResetTarget();
                return false; 
            }
            Health healthCheack = combatTarget.GetComponent<Health>();                        
            return healthCheack && healthCheack.HealthPoints > 0/*OR targetHealth.IsAlive?????*/ && combatTarget.name != this.name;
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

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            SetupWeapon((string)state);
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.Damage;
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)//???????
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.PercentageBonus;
            }
        }
    }
}
