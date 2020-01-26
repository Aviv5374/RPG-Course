using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;
using RPG.Characters;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent myMeshAgent;
        CharacterAnimatorHandler myAnimator;

        Health health;
        Fighter fighter;
        ActionScheduler actionScheduler;

        // Start is called before the first frame update
        void Start()
        {
            myMeshAgent = GetComponent<NavMeshAgent>();
            myAnimator = GetComponent<CharacterAnimatorHandler>();

            health = GetComponent<Health>();

            fighter = GetComponent<Fighter>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        // Update is called once per frame
        void Update()
        {
            myMeshAgent.enabled = health.IsAlive;
            UpdateAnimator();
        }
        
        public void StartMoveAction(Vector3 destination)//?????
        {
            ///actionScheduler.StartAction(this);            
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            myMeshAgent.isStopped = false;
            myMeshAgent.destination = destination;
        }

        public void StopMoving()
        {
            myMeshAgent.isStopped = true;            
        }

        void UpdateAnimator()
        {
            Vector3 velocity = myMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            myAnimator.UpdateLocomotion(speed);
        }
        
        public void CancelAction()
        {
            StopMoving();
        }
    }
}
