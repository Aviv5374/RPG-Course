using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;

    NavMeshAgent myMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        myMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        myMeshAgent.destination = target.position;
    }
}
