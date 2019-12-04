using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        NavMeshAgent myMeshAgent;
        Animator myAnimator;
        List<AnimatorControllerParameter> myAnimtorParameters = new List<AnimatorControllerParameter>();

        // Start is called before the first frame update
        void Start()
        {
            myMeshAgent = GetComponent<NavMeshAgent>();
            SetMyAnimator();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        void SetMyAnimator()//TODO:FINE Better Method Name
        {
            myAnimator = GetComponent<Animator>();
            for (int index = 0; index < myAnimator.parameterCount; index++)
            {
                myAnimtorParameters.Add(myAnimator.GetParameter(index));
            }
        }

        public void StartMoveAction(Vector3 destination)//?????
        {
            //GetComponent<Fighter>().Cancel();
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
            UpdateLocomotion(speed);
        }

        void UpdateLocomotion(float speed)
        {
            myAnimator.SetFloat(myAnimtorParameters[0].name, speed);
        }
    }
}
