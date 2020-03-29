using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] Transform target = null;
        [SerializeField] float speed = 1f;

        CapsuleCollider targetCapsule = null;

        CapsuleCollider TargetCapsule 
        { 
            get 
            {
                if (targetCapsule == null)
                {
                    targetCapsule = target.GetComponent<CapsuleCollider>();
                }

                return targetCapsule;
            } 
        }

        #region Initialization

        void Awake()
        {

        }

        void Start()
        {

        }

        #endregion

        #region Updating	 

        void Update()
        {
            if (!target) { return; }
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        #endregion

        private Vector3 GetAimLocation()
        {           
            if (TargetCapsule == null)
            {
                return target.position;
            }
            return target.position + Vector3.up * TargetCapsule.height / 2;
        }

    }
}