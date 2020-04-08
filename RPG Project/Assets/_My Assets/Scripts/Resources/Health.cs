using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Characters;
using RPG.Core;
using RPG.Saving;
using RPG.My.Saving;
using RPG.Stats;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable, IMySaveable
    {
        [SerializeField] float healthPoints = -1f;
        [SerializeField] float regenerationPercentage = 70;
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

        public float HealthPoints { get { return healthPoints; } }
        public bool IsAlive { get { return healthPoints > 0; } }//????
        public bool IsDead { get { return healthPoints <= 0; } }//????
        public float HealthPercentage { get { return 100 * (healthPoints / myBaseStats.GetStat(Stat.Health)); } }
        float RegenHealthPoints { get { return myBaseStats.GetStat(Stat.Health) * (regenerationPercentage / 100); } }

        public event Action onDeathTest;

        void Awake()
        {
            SetComponent();
        }

        void Start()
        {
            myBaseStats.onLevelUp += RegenerateHealth;

            if (healthPoints < 0)
            {
                RegenerateFullHealth();
            }

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

        void RegenerateFullHealth()
        {
            healthPoints = myBaseStats.GetStat(Stat.Health);
        }

        void RegenerateHealth()
        {            
            healthPoints = Mathf.Max(healthPoints, RegenHealthPoints);
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            if (IsAlive)
            {
                healthPoints = Mathf.Max(healthPoints - damage, 0);
                //Debug.Log(name + " TakeDamage Form " + instigator.name);
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
                onDeathTest();
                SetComponent();
                myAnimator.TriggerDeath();                
                actionScheduler.CancelCurrentAction();//?????                
            }
        }

        void DeathCheack(GameObject instigator)
        {
            if (IsDead)
            {
                onDeathTest();
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
            healthPoints = (float)state;

            DeathCheack();
        }
    }
}
