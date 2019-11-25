using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;

    Ray lastRay;
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
            lastRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        }
        Debug.DrawRay(lastRay.origin, lastRay.direction * 100, Color.red);
        myMeshAgent.destination = target.position;      
    }
}
