using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;
using RPG.Characters;
using RPG.Saving;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float injuredSpeed = 1.5f;
        [SerializeField] float walkSpeed = 4f;
        [SerializeField] float runSpeed = 6f;

        NavMeshAgent myMeshAgent;
        CharacterAnimatorHandler myAnimator;

        Health health;
        Fighter fighter;
        ActionScheduler actionScheduler;

        // Start is called before the first frame update
        void Start()
        {
            myMeshAgent = GetComponent<NavMeshAgent>();
            myMeshAgent.speed = walkSpeed;

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

        public void MoveTo(Vector3 destination, bool isInjured = false, bool isChasing = false)
        {
            myMeshAgent.isStopped = false;
            myMeshAgent.destination = destination;
            SetAgentSpeed(isInjured, isChasing);
        }

        private void SetAgentSpeed(bool isInjured, bool isChasing)
        {
            if (isChasing)
            {
                myMeshAgent.speed = runSpeed;
            }
            else if (isInjured)
            {
                myMeshAgent.speed = injuredSpeed;
            }
            else
            {
                myMeshAgent.speed = walkSpeed;
            }
        }

        void UpdateAnimator()
        {
            Vector3 velocity = myMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            myAnimator.UpdateLocomotion(speed);
        }

        public void StopMoving()
        {
            myMeshAgent.speed = walkSpeed;
            myMeshAgent.isStopped = true;            
        }
        
        public void CancelAction()
        {
            StopMoving();
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 statePos = (SerializableVector3)state;
            myMeshAgent.Warp(statePos.ToVector());
        }
    }
}
