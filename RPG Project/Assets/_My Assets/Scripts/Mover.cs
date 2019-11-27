using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    //[SerializeField] Transform target;
   
    Camera mainCamera;
    NavMeshAgent myMeshAgent;
    Animator myAnimator;
    List<AnimatorControllerParameter> myAnimtorParameters = new List<AnimatorControllerParameter>();

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        myMeshAgent = GetComponent<NavMeshAgent>();
        SetMyAnimator();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCurser();
        }

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

    void MoveToCurser()
    {
        RaycastHit hitInfo;
        Ray lastRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        bool hasHit = Physics.Raycast(lastRay,out hitInfo);
        if (hasHit)
        {
            myMeshAgent.destination = hitInfo.point;            
        }
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
