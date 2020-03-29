using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;

        Health target = null;
        CapsuleCollider targetCapsule = null;

        CapsuleCollider TargetCapsule 
        { 
            get 
            {
                if (targetCapsule == null)
                {
                    targetCapsule = Target.GetComponent<CapsuleCollider>();
                }

                return targetCapsule;
            } 
        }

        public Health Target { get => target; set => target = value; }

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
            if (!Target) { return; }
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        #endregion

        private Vector3 GetAimLocation()
        {           
            if (TargetCapsule == null)
            {
                return Target.transform.position;
            }
            return Target.transform.position + Vector3.up * TargetCapsule.height / 2;
        }

    }
}