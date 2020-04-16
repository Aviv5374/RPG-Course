using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Attributes;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3.5f;
        [SerializeField] float agroCooldownTime = 5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 0.51f;
        [SerializeField] float waypointDwellTime = 2.75f;

        PlayerController player;
        Mover mover;
        Health health;
        Fighter fighter;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtGuardPos = Mathf.Infinity;
        float timeSinceAggrevated = Mathf.Infinity;
        int currentWaypointIndex = 0;

        float DistanceFromPlayer { get { return Vector3.Distance(this.transform.position, player.transform.position); } }
        bool IsAggrevated { get { return timeSinceAggrevated < agroCooldownTime; } }
        bool InAttackRangeOfPlayer { get { return DistanceFromPlayer <= chaseDistance; } }
        bool IsAttackIsTrigger { get { return IsAggrevated || InAttackRangeOfPlayer; } }
        Vector3 CurrentWaypoint { get { return patrolPath.GetWayPointPos(currentWaypointIndex); } }
        bool IfLoseSightOfPlayer { get { return timeSinceLastSawPlayer < suspicionTime; } }
        bool OnGuardDuty { get { return AtGuardPosition() && timeSinceArrivedAtGuardPos < waypointDwellTime; } }


        //bool IsCheckingSometingSuspicion { get}

        void Awake()
        {            
            player = FindObjectOfType<PlayerController>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
        }

        void Start()
        {
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
            if (IsAttackIsTrigger && fighter.CanAttack(player.CombatTarget))
            {
                AttackBehaviour();

            }
            else if (IfLoseSightOfPlayer || OnGuardDuty)
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

            UpdateTimers();
        }

        public void Aggrevate()
        {
            timeSinceAggrevated = 0;
        }

        void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtGuardPos += Time.deltaTime;
            timeSinceAggrevated += Time.deltaTime;
        }

        void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            //Debug.Log(name + " can chase player!!!");
            fighter.Attack(player.CombatTarget, false, true);
        }

        void SuspicionBehaviour()
        {
            fighter.CancelAttack();//OR GetComponent<ActionScheduler>().CancelCurrentAction();
            mover.StopMoving();
            //CanDo: Add Animation.
        }

        void GuardBehaviour()
        {
            mover.MoveTo(guardPosition);//OR mover.StartMoveAction(guardPosition);
            //CanDo: Add Animation.
        }

        void PotrolBehaviour()
        {
            if (AtGuardPosition())
            {
                CycleWaypoint();
                timeSinceArrivedAtGuardPos = 0;
            }
            guardPosition = CurrentWaypoint;

            GuardBehaviour();
        }

        bool AtGuardPosition()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, guardPosition);
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

