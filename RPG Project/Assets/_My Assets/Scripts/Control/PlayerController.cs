﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using RPG.Characters.Player;
using RPG.Resources;

namespace RPG.Control
{
    public class PlayerController : CharacterController
    {
        enum CursorType
        {
            None,
            Movement,
            Combat
        }

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;

        Camera mainCamera;
        
        ActionScheduler actionScheduler;
        CombatTarget myCombatTarget;
        //PlayerAnimatorHandler myAnimator;????

        Ray MouseRay { get { return mainCamera.ScreenPointToRay(Input.mousePosition); } }

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
            if (health.IsDead) return;

            //DOTO: try to Raycast once for all uses
            if (InteractWithCombat()) { return; }
            if (InteractWithMovement()) { return; }

            SetCursor(CursorType.None);
        }

        bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(MouseRay);
            foreach (RaycastHit hitInfo in hits)
            {
                //Debug.Log("print from InteractWithCombat() the hitInfo of " + hitInfo.transform.gameObject.name);
                CombatTarget target = hitInfo.transform.GetComponent<CombatTarget>();
                if (!fighter.CanAttack(target)) continue;

                if (Input.GetMouseButton(0))
                {
                    actionScheduler.StartAction(fighter);//OR mover.StopMoving();????
                    fighter.Attack(target);
                    //TODO: I want that myAnimator.TriggerAttack() be here, not in fighter.
                }
                SetCursor(CursorType.Combat);
                return true;
            }

            return false;
        }

        bool InteractWithMovement()
        {           
            RaycastHit hitInfo;           
            bool hasHit = Physics.Raycast(MouseRay, out hitInfo);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    actionScheduler.StartAction(mover);//OR fighter.CancelAttack();                     
                    mover.StartMoveAction(hitInfo.point);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

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