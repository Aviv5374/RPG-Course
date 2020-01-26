using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        PlayerController player;

        void Awake()
        {

        }

        void Start()
        {
            player = FindObjectOfType<PlayerController>();
        }

        void Update()
        {
            float DistanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);
            if (DistanceFromPlayer <= chaseDistance)
            {
                Debug.Log(name + " can chase player!!!");
            }
        }

    }
}