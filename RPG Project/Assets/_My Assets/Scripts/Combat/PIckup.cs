using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.Combat
{
    public class Pickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] protected float respawnTime = 5;

        Collider myCollider;
        List<Transform> myChildrens = new List<Transform>();

        #region Initialization

        protected void Awake()
        {
            myCollider = GetComponent<Collider>();
            for (int i = 0; i < transform.childCount; i++)
            {
                myChildrens.Add(transform.GetChild(i));
            }
        }

        void Start()
        {

        }

        #endregion

        #region Updating	 

        void Update()
        {

        }

        #endregion
       
        protected virtual void OnTriggerEnter(Collider other)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player)
            {
                InvokePickup(player);
            }
        }

        protected virtual void InvokePickup(PlayerController player)
        {            
            StartCoroutine(HideForSeconds(respawnTime));
        }

        protected IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        protected void ShowPickup(bool shouldShow)
        {
            myCollider.enabled = shouldShow;
            for (int i = 0; i < myChildrens.Count; i++)
            {
                myChildrens[i].gameObject.SetActive(shouldShow);
            }
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }

        public bool HandleRaycast(PlayerController player)
        {
            if (Input.GetMouseButtonDown(0))
            {
                InvokePickup(player);
            }
            return true;
        }

    }
}