using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    //[SerializeField] Transform target;
   
    Camera mainCamera;
    NavMeshAgent myMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        myMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCurser();
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
}
