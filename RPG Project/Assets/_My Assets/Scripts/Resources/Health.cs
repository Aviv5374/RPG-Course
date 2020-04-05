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
        [SerializeField] float healthPoints = 100f;
        CharacterAnimatorHandler myAnimator;
        ActionScheduler actionScheduler;

        public float HealthPoints { get { return healthPoints; } }
        public bool IsAlive { get { return healthPoints > 0; } }//????
        public bool IsDead { get { return healthPoints <= 0; } }//????

        void Start()
        {
            SetComponent();
            healthPoints = GetComponent<BaseStats>().GetHealth();
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
        }

        public void TakeDamage(float damage)
        {
            if (IsAlive)
            {
                healthPoints = Mathf.Max(healthPoints - damage, 0);
                //Debug.Log(name + " health " + healthPoints);
                DeathCheack();
            }
        }

        void DeathCheack()
        {
            if (IsDead)
            {
                SetComponent();
                myAnimator.TriggerDeath();                
                actionScheduler.CancelCurrentAction();//?????
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
