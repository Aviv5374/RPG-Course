using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming = false;

        float damage = 0;
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
        public float Damage { get => damage; set => damage = value; }

        #region Initialization

        void Awake()
        {

        }

        void Start()
        {
            transform.LookAt(GetAimLocation());
            Destroy(gameObject, 5.5f);
        }

        #endregion

        #region Updating	 

        void Update()
        {
            if (target && target.IsAlive && isHoming) 
            {
                transform.LookAt(GetAimLocation());                 
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() == target && target.IsAlive)
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
        }

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