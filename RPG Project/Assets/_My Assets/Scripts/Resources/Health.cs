using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Utils;
using RPG.Characters;
using RPG.Core;
using RPG.Saving;
using RPG.My.Saving;
using RPG.Stats;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable, IMySaveable
    {
        [SerializeField] LazyValue<float> healthPoints;
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

        public float HealthPoints 
        { 
            get { return healthPoints.value; } 

            private set { healthPoints.value = value; }
        }

        public bool IsAlive { get { return HealthPoints > 0; } }//????
        public bool IsDead { get { return HealthPoints <= 0; } }//????
        public float MaxHealthPoints { get { return myBaseStats.GetStat(Stat.Health); } }
        public float HealthPercentage { get { return 100 * (HealthPoints / MaxHealthPoints); } }
        float RegenHealthPoints { get { return MaxHealthPoints * (regenerationPercentage / 100); } }

        public event Action onDeathTest;

        void Awake()
        {
            SetComponent();
            healthPoints = new LazyValue<float>(RegenerateFullHealth);
        }

        void OnEnable()
        {
            myBaseStats.onLevelUp += RegenerateHealth;
        }

        void Start()
        {
            healthPoints.ForceInit();
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

        public void TakeDamage(GameObject instigator, float damage)
        {
            if (IsAlive)
            {
                HealthPoints = Mathf.Max(HealthPoints - damage, 0);
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
