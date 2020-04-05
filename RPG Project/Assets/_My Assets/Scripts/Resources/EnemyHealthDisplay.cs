using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Control;
using RPG.Combat;

namespace RPG.Resources
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        [SerializeField] Text healthValueText = null;

        Fighter playerFighter;
        Health enemyHealth;

        #region Initialization

        void Awake()
        {

        }

        void Start()
        {
            playerFighter = FindObjectOfType<PlayerController>().GetComponent<Fighter>();           
        }

        #endregion

        #region Updating	 

        void Update()
        {
            enemyHealth = playerFighter.TargetHealth;
            if (!enemyHealth)
            {
                healthValueText.text = "N/A";                
            }
            else
            {
                healthValueText.text = string.Format("{0:0.0}%", enemyHealth.HealthPercentage);
            }
        }

        #endregion

    }
}