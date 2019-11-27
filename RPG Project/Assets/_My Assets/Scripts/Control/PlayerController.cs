using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Camera mainCamera;
        Mover mover;

        void Start()
        {
            mainCamera = Camera.main;
            mover = GetComponent<Mover>();
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        void MoveToCursor()
        {
            RaycastHit hitInfo;
            Ray lastRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(lastRay, out hitInfo);
            if (hasHit)
            {
                mover.MoveTo(hitInfo.point);
            }
        }
    }
}