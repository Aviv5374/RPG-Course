using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using RPG.Characters.Player;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Camera mainCamera;
        Mover mover;
        Fighter fighter;
        ActionScheduler actionScheduler;
        //PlayerAnimatorHandler myAnimator;????

        Ray MouseRay { get { return mainCamera.ScreenPointToRay(Input.mousePosition); } }

        void Start()
        {
            mainCamera = Camera.main;
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            actionScheduler = GetComponent<ActionScheduler>();
            //myAnimator = GetComponent<PlayerAnimatorHandler>();?????
        }

        void Update()
        {
            //DOTO: try to Raycast once for all uses
            if (InteractWithCombat()) { return; }
            if (InteractWithMovement()) { return; }                       
        }

        bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(MouseRay);
            foreach (RaycastHit hitInfo in hits)
            {
                //Debug.Log("print from InteractWithCombat() the hitInfo of " + hitInfo.transform.gameObject.name);
                CombatTarget target = hitInfo.transform.GetComponent<CombatTarget>();
                if (!target) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    actionScheduler.StartAction(fighter);//OR mover.StopMoving();????
                    fighter.Attack(target);
                    //TODO: I want that myAnimator.TriggerAttack() be here, not in fighter.
                }
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
                return true;
            }
            return false;
        }
    }
}