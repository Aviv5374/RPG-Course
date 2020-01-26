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
        Mover mover;
        Health health;
        Fighter fighter;

        Vector3 guardPosition;

        float DistanceFromPlayer { get { return Vector3.Distance(this.transform.position, player.transform.position); } }
        bool InAttackRangeOfPlayer { get { return DistanceFromPlayer <= chaseDistance; } }
        
        void Start()
        {
            player = FindObjectOfType<PlayerController>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();

            guardPosition = transform.position;
        }

        void Update()
        {
            if (health.IsDead) 
            {
                fighter.CancelAttack();
                return; 
            }

            if (InAttackRangeOfPlayer && fighter.CanAttack(player.CombatTarget))
            {
                //Debug.Log(name + " can chase player!!!");
                fighter.Attack(player.CombatTarget);
            }
            else
            {
                fighter.CancelAttack();
                mover.MoveTo(guardPosition);//OR mover.StartMoveAction(guardPosition);
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;            
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

    }
}