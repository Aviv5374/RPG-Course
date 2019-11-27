using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Camera mainCamera;
        Mover mover;
        Fighter fighter;

        Ray MouseRay { get { return mainCamera.ScreenPointToRay(Input.mousePosition); } }

        void Start()
        {
            mainCamera = Camera.main;
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        void Update()
        {
            //DOTO: try to Raycast once for all uses
            InteractWithCombat();
            InteractWithMovement();            
        }

        private void InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(MouseRay);
            foreach (RaycastHit hitInfo in hits)
            {
                //Debug.Log(hitInfo.transform.gameObject.name);
                CombatTarget target = hitInfo.transform.GetComponent<CombatTarget>();

                if (target && Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
            } 
        }

        void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        void MoveToCursor()
        {
            RaycastHit hitInfo;           
            bool hasHit = Physics.Raycast(MouseRay, out hitInfo);
            if (hasHit)
            {
                mover.MoveTo(hitInfo.point);
            }
        }
    }
}