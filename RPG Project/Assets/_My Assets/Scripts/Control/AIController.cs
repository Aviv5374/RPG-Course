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
        [SerializeField] float suspicionTime = 3.5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;

        PlayerController player;
        Mover mover;
        Health health;
        Fighter fighter;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        int currentWaypointIndex = 0;

        float DistanceFromPlayer { get { return Vector3.Distance(this.transform.position, player.transform.position); } }
        bool InAttackRangeOfPlayer { get { return DistanceFromPlayer <= chaseDistance; } }
        Vector3 CurrentWaypoint { get { return patrolPath.GetWayPointPos(currentWaypointIndex); } }

        void Start()
        {
            player = FindObjectOfType<PlayerController>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();

            if (patrolPath)
            {
                transform.position = CurrentWaypoint;
            }

            guardPosition = transform.position;
        }

        void Update()
        {
            if (health.IsDead)
            {
                fighter.CancelAttack();
                return;
            }

            MainBehavior();
        }

        void MainBehavior()
        {
            if (InAttackRangeOfPlayer && fighter.CanAttack(player.CombatTarget))
            {
                AttackBehaviour();

            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                if (patrolPath)
                {
                    PotrolBehaviour();
                }
                else
                {
                    GuardBehaviour();
                }
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            //Debug.Log(name + " can chase player!!!");
            fighter.Attack(player.CombatTarget);
        }

        void SuspicionBehaviour()
        {
            fighter.CancelAttack();//OR GetComponent<ActionScheduler>().CancelCurrentAction();
            //CanDo: Add Animation.
        }

        void GuardBehaviour()
        {
            mover.MoveTo(guardPosition);//OR mover.StartMoveAction(guardPosition);
            //CanDo: Add Animation.
        }

        void PotrolBehaviour()
        {
            Vector3 nextPosition;

            if (AtWaypoint())
            {
                CycleWaypoint();
            }
            nextPosition = CurrentWaypoint;

            mover.MoveTo(nextPosition);//OR mover.StartMoveAction(guardPosition);
        }

        bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, CurrentWaypoint);
            return distanceToWaypoint < waypointTolerance;
        }

        void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }
        
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

    }
}

