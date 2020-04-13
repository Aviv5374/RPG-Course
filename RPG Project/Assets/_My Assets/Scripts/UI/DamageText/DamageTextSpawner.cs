using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {

        [SerializeField] DamageText damageTextPrefab = null;

        #region Initialization

        void Awake()
        {

        }

        void Start()
        {
            
        }

        #endregion

        public void Spawn(float damageAmount)
        {
            DamageText instance = Instantiate(damageTextPrefab, transform);
        }


        #region Updating	 

        void Update()
        {

        }

        #endregion

    }
}