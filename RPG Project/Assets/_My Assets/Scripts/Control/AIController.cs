using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        PlayerController player;
        Mover mover;//?????
        Fighter fighter;

        float DistanceFromPlayer { get { return Vector3.Distance(this.transform.position, player.transform.position); } }
        bool InAttackRangeOfPlayer { get { return DistanceFromPlayer <= chaseDistance; } }

        void Awake()
        {

        }

        void Start()
        {
            player = FindObjectOfType<PlayerController>();
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        void Update()
        {           
            if (InAttackRangeOfPlayer && fighter.CanAttack(player.CombatTarget))
            {
                //Debug.Log(name + " can chase player!!!");
                fighter.Attack(player.CombatTarget);
            }
            else
            {
                fighter.CancelAttack();
            }
        }

    }
}