using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    //[ExecuteInEditMode]????????
    public class PotrolPath : MonoBehaviour
    {
        [SerializeField] float waypointGizmoRadius = 0.5f;

        /* More efficient at performance level. Not updated on any changes.
        List<Transform> wayPoints = new List<Transform>();

        void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                wayPoints.Add(transform.GetChild(i));
            }
        }
        */

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform wayPoint = transform.GetChild(i);
                //Gizmos.color = Color.white;
                Gizmos.DrawSphere(wayPoint.position, waypointGizmoRadius);
            }
        }

    }
}