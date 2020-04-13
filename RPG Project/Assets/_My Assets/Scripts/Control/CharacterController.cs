using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using RPG.Resources;

namespace RPG.Control
{
    public class CharacterController : MonoBehaviour
    {
        protected Mover mover;
        protected Health health;
        protected Fighter fighter = null;

        public Fighter Fighter 
        {
            get 
            {
                if (!fighter)
                {
                    fighter = GetComponent<Fighter>();
                }
                return fighter;
            }
        }

        protected virtual void Awake()
        {
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
        }

        protected virtual void OnEnable()
        {
            health.onDeathTest += DeathTest;
            health.onDeathTest += CharacterDeathTest;
        }

        protected virtual void Start()
        {
            
        }

        protected virtual void OnDisable()
        {
            health.onDeathTest -= DeathTest;
            health.onDeathTest -= CharacterDeathTest;
        }

        protected virtual void DeathTest()
        {
            Debug.Log("DeathTest in " + typeof(CharacterController).Name);
        }

        void CharacterDeathTest()
        {
            Debug.Log("DeathTest in Character");
        }

    }
}