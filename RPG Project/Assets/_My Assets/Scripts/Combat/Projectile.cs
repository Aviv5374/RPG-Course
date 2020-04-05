using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Resources;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 4.5f;
        [SerializeField] bool isHoming = false;
        [SerializeField] ParticleSystem hitEffect = null;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeAfterImpact = 1.25f;

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
            Destroy(gameObject, maxLifeTime);
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
                speed = 0;

                if (hitEffect)
                {
                    ParticleSystem effct = Instantiate(hitEffect, GetAimLocation(), transform.rotation);
                    //Destroy(effct, effct.main.duration + 1.25f);//OR The Code in DestroyAfterEffect
                }

                for (int i = 0; i < destroyOnHit.Length; i++)
                {
                    Destroy(destroyOnHit[i]);
                }

                Destroy(gameObject, lifeAfterImpact);
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