using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;
using RPG.Characters;
using RPG.Saving;
using RPG.My.Saving;
using RPG.Resources;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable, IMySaveable
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

        #region SavingSystem
        [System.Serializable]
        struct MoverSaveData
        {
            public SerializableVector3 POS;
            public SerializableVector3 ROT;
        }

        public object CaptureState()
        {
            #region OPTION ONE
            //Dictionary<string, object> data1 = new Dictionary<string, object>();
            //data1["POS"] = new SerializableVector3(transform.position);
            //data1["ROT"] = new SerializableVector3(transform.eulerAngles);
            #endregion
            //OR
            MoverSaveData data2 = new MoverSaveData();
            data2.POS = new SerializableVector3(transform.position);
            data2.ROT = new SerializableVector3(transform.eulerAngles);

            return data2;
        }

        public void RestoreState(object state)
        {
            #region OPTION ONE
            /*
            Dictionary<string, object> data1 = (Dictionary<string, object>)state;
            Vector3 newPos = ((SerializableVector3)data1["POS"]).ToVector();
            Vector3 newEulerAngles = ((SerializableVector3)data1["ROT"]).ToVector();
            GetComponent<NavMeshAgent>().Warp(newPos);
            transform.eulerAngles = newEulerAngles;
            */
            #endregion
            //OR
            MoverSaveData data2 = (MoverSaveData)state;
            GetComponent<NavMeshAgent>().Warp(data2.POS.ToVector());
            transform.eulerAngles = data2.ROT.ToVector();
        }
        #endregion
    }
}
