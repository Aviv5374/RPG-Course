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
        [SerializeField] bool showPercent = false;

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
            string format;
            if (!enemyHealth)
            {
                format = "N/A";                
            }
            else
            {
                if (showPercent)
                {
                    format = string.Format("{0:0.0}%", enemyHealth.HealthPercentage);
                }
                else
                {
                    format = string.Format("{0:0}/{1:0}", enemyHealth.HealthPoints, enemyHealth.MaxHealthPoints);
                }
            }
            healthValueText.text = format;
        }

        #endregion

    }
}