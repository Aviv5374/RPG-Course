using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Characters;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        CharacterAnimatorHandler myAnimator;
        public float HealthPoints { get { return healthPoints; } }

        void Start()
        {
            myAnimator = GetComponent<CharacterAnimatorHandler>();
        }


        public void TakeDamage(float damage)
        {
            if (healthPoints > 0)
            {
                healthPoints = Mathf.Max(healthPoints - damage, 0);
                Debug.Log(name + " health " + healthPoints);
                if (healthPoints <= 0)
                {
                    myAnimator.TriggerDeath();//????
                }
            }                           
        }

    }
}