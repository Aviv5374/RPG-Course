﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 5;

        Collider myCollider;
        List<Transform> myChildrens = new List<Transform>();

        void Awake()
        {
            myCollider = GetComponent<Collider>();
            for (int i = 0; i < transform.childCount; i++)
            {
                myChildrens.Add(transform.GetChild(i));
            }
        }

        //I prefer EquipWeapon() be called through the PlayerController rather than directly through Fighter
        void OnTriggerEnter(Collider other)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player)
            {
                Pickup(player.Fighter);                
            }
        }

        void Pickup(Fighter fighter)
        {
            fighter.EquipWeapon(weapon);
            StartCoroutine(HideForSeconds(respawnTime));
        }

        IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        void ShowPickup(bool shouldShow)
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

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Pickup(callingController.GetComponent<Fighter>());
            }
            return true;
        }
    }
}