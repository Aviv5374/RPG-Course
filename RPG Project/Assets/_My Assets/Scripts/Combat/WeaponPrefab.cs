using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPrefab : MonoBehaviour
    {
       public void OnHit()
       {
            Debug.Log("Weapon Hit " + name);
        }
    }
}