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
        protected Fighter fighter;

        protected virtual void Awake()
        {
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
        }

        protected virtual void Start()
        {
            health.onDeathTest += DeathTest;
            health.onDeathTest += CharacterDeathTest;
        }

        void Update()
        {

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