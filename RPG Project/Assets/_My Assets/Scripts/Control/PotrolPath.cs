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
            for (int index = 0; index < transform.childCount; index++)
            {
                //Gizmos.color = Color.white;
                Gizmos.DrawSphere(GetWayPointPos(index), waypointGizmoRadius);
                Gizmos.DrawLine(GetWayPointPos(index), GetWayPointPos(GetNextIndex(index)));
            }
        }

        Vector3 GetWayPointPos(int i)
        {
            return transform.GetChild(i).position;
        }

        int GetNextIndex(int index)
        {
            if (index < transform.childCount-1)
            {
                return index + 1;
            }
            else
            {
                return 0;
            }
        }

    }
}