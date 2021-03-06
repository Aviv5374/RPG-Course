﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameDevTV.Utils;
using RPG.Characters;
using RPG.Core;
using RPG.Saving;
using RPG.My.Saving;
using RPG.Stats;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable, IMySaveable
    {
        [System.Serializable]
        class TakeDamageEvent : UnityEvent<float>
        {
        }

        public event Action takeDamageDelegate;
        
        [SerializeField] TakeDamageEvent takeDamageEvent;
        [SerializeField] UnityEvent onDie;
        [SerializeField] float healthPoints = -1;
        [SerializeField] float regenerationPercentage = 70;

        LazyValue<float> healthPointsLazyValue;

        CharacterAnimatorHandler myAnimator;
        ActionScheduler actionScheduler;
        BaseStats myBaseStats;

        //Alternative idea to SetComponent()
        CharacterAnimatorHandler MyAnimator 
        {
            get 
            {
                if (!myAnimator)
                {
                    myAnimator = GetComponent<CharacterAnimatorHandler>();
                }
                return myAnimator;
            }
        }

        public float HealthPoints 
        { 
            get { return healthPoints; } 

            private set { healthPoints = value; }
        }

        public float HealthPointsLazyValue
        {
            get { return healthPointsLazyValue.value; }

            private set { healthPointsLazyValue.value = value; }
        }

        public bool IsAlive { get { return HealthPoints > 0; } }//????
        public bool IsDead { get { return HealthPoints <= 0; } }//????
        public float MaxHealthPoints { get { return myBaseStats.GetStat(Stat.Health); } }
        public float HealFraction { get { return HealthPoints / MaxHealthPoints; } }
        public float HealthPercentage { get { return 100 * HealFraction; } }
        float RegenHealthPoints { get { return MaxHealthPoints * (regenerationPercentage / 100); } }

        public event Action onDeathTest;

        void Awake()
        {
            SetComponent();
            healthPointsLazyValue = new LazyValue<float>(RegenerateFullHealth);
        }

        void OnEnable()
        {
            myBaseStats.onLevelUp += RegenerateHealth;
        }

        void Start()
        {
            if (HealthPoints < 0)
            {
                HealthPoints = MaxHealthPoints;
            }
            healthPointsLazyValue.ForceInit();
        }

        void OnDisable()
        {
            myBaseStats.onLevelUp -= RegenerateHealth;
        }

        void SetComponent()
        {
            if (!myAnimator)
            {
                myAnimator = GetComponent<CharacterAnimatorHandler>();
            }
            if (!actionScheduler)
            {
                actionScheduler = GetComponent<ActionScheduler>();
            }
            if (!myBaseStats)
            {
                myBaseStats = GetComponent<BaseStats>();
            }
        }

        float RegenerateFullHealth()
        {
           return MaxHealthPoints;
        }

        void RegenerateHealth()
        {            
            HealthPoints = Mathf.Max(HealthPoints, RegenHealthPoints);
        }

        public void Heal(float healthToRestore)
        {
            HealthPoints = Mathf.Min(healthPoints + healthToRestore, MaxHealthPoints);
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            if (IsAlive)
            {
                HealthPoints = Mathf.Max(HealthPoints - damage, 0);
                //Debug.Log(name + " TakeDamage Form " + instigator.name);
                takeDamageEvent.Invoke(damage);
                if (IsDead)
                {
                    onDie.Invoke();
                }
                //takeDamageDelegate();
                DeathCheack(instigator);                
            }
        }

        void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) { return; }

            experience.GainExperience(myBaseStats.GetStat(Stat.ExperienceReward));
        }

        void DeathCheack()
        {
            if (IsDead)
            {
                //onDeathTest();
                SetComponent();
                myAnimator.TriggerDeath();                
                actionScheduler.CancelCurrentAction();//?????                
            }
        }

        void DeathCheack(GameObject instigator)
        {
            if (IsDead)
            {
                //onDeathTest();
                SetComponent();
                myAnimator.TriggerDeath();
                actionScheduler.CancelCurrentAction();//?????
                AwardExperience(instigator);
            }
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            HealthPoints = (float)state;

            DeathCheack();
        }
    }
}
