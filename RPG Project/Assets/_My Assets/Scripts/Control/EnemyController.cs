using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using RPG.Characters.Enemy;
using RPG.Attributes;

namespace RPG.Control
{
    public class EnemyController : MonoBehaviour
    {
        EnemyAnimatorHandler myAnimator;//????
        Health myHealth;

        void Awake()
        {

        }

        void Start()
        {
            myAnimator = GetComponent<EnemyAnimatorHandler>();//?????
            myHealth = GetComponent<Health>(); 
        }

        void Update()
        {
            //if (myHealth.HealthAmount <= 0)
            //{
            //    myAnimator.TriggerDeath();
            //}
        }

    }
}