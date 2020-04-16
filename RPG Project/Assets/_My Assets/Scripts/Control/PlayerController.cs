using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using RPG.Characters.Player;
using RPG.Attributes;

namespace RPG.Control
{
    public class PlayerController : CharacterController
    {        
        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float maxNavMeshProjectionDistance = 1f;        

        Camera mainCamera;
        
        ActionScheduler actionScheduler;
        CombatTarget myCombatTarget;
        //PlayerAnimatorHandler myAnimator;????

        Ray MouseRay { get { return mainCamera.ScreenPointToRay(Input.mousePosition); } }

        public ActionScheduler ActionScheduler
        {
            get
            {
                if (!actionScheduler)
                {
                    actionScheduler = GetComponent<ActionScheduler>();
                }
                return actionScheduler;
            }
        }

        public CombatTarget CombatTarget { get { return myCombatTarget; } }

        protected override void Awake()
        {
            base.Awake();
            mainCamera = Camera.main;            
            actionScheduler = GetComponent<ActionScheduler>();
            myCombatTarget = GetComponent<CombatTarget>();
            //myAnimator = GetComponent<PlayerAnimatorHandler>();?????
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            health.onDeathTest += PlayerDeathTest;
            //health.onDeathTest += DeathTest;
            //health.takeDamage += DamageTest;
        }

        void DamageTest(float LOL)
        {

        }

        protected override void Start()
        {
            base.Start();
            
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            health.onDeathTest -= PlayerDeathTest;
            //health.onDeathTest -= DeathTest;
        }

        void Update()
        {
            if (InteractWithUI()) { return; }
            if (health.IsDead) 
            {
                SetCursor(CursorType.Dead);
                return; 
            }

            //DOTO: try to Raycast once for all uses
            if (InteractWithComponent()) { return; }
            if (InteractWithMovement()) { return; }

            SetCursor(CursorType.None);
        }

        bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject()) 
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        bool InteractWithComponent()
        {
            RaycastHit[] hitsInfo = RaycastAllSorted();
            for (int index1 = 0; index1 < hitsInfo.Length; index1++)
            {
                IRaycastable[] raycastables = hitsInfo[index1].transform.GetComponents<IRaycastable>();
                for (int index2 = 0; index2 < raycastables.Length; index2++)
                {
                    if (raycastables[index2].HandleRaycast(this))
                    {
                        SetCursor(raycastables[index2].GetCursorType());
                        return true;
                    }
                }
            }            
            return false;
        }

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.RaycastAll(MouseRay);
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }

        #region Movement

        bool InteractWithMovement()
        {
            Vector3 target;
            bool hasHit = RaycastNavMesh(out target);
            if (hasHit)
            {
                if (!mover.CanMoveTo(target)) return false;

                if (Input.GetMouseButton(0))
                {
                    actionScheduler.StartAction(mover);//OR fighter.CancelAttack();                   
                    mover.StartMoveAction(target);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();

            RaycastHit hit;
            bool hasHit = Physics.Raycast(MouseRay, out hit);
            if (!hasHit) return false;

            NavMeshHit navMeshHit;
            bool hasCastToNavMesh = NavMesh.SamplePosition(
                hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);
            if (!hasCastToNavMesh) return false;

            target = navMeshHit.position;            
            return true;
        }
        
        #endregion

        void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        CursorMapping GetCursorMapping(CursorType type)
        {
            for (int i = 0; i < cursorMappings.Length; i++)
            {
                if (cursorMappings[i].type == type)
                {
                    return cursorMappings[i];
                }
            }
            return cursorMappings[0];
        }

        public void StopMoving()
        {
            mover.StopMoving();
        }

        public void CancelAttack()
        {
            fighter.CancelAttack();
        }

        public void CancelCurrentAction()
        {
            actionScheduler.CancelCurrentAction();
        }

        protected override void DeathTest()
        {
            Debug.Log("DeathTest2 in " + typeof(PlayerController).Name);
            base.DeathTest();
        }

        void PlayerDeathTest()
        {
            Debug.Log("DeathTest in Player");
        }
    }
}