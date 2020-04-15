using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class WeaponPrefab : MonoBehaviour
    {
        [SerializeField] UnityEvent onHit;

        public void OnHit()
        { 
            Debug.Log("Weapon Hit " + name);
            onHit.Invoke();
        }
    }
}