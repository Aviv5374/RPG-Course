using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Control;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] Text healthValueText = null;
        [SerializeField] bool showPercent = false;

        Health playerHealth;

        #region Initialization

        void Awake()
        {
            playerHealth = FindObjectOfType<PlayerController>().GetComponent<Health>();
            //healthValueText = GetComponent<Text>();//?????
        }

        void Start()
        {
        }

        #endregion

        #region Updating	 

        void Update()
        {
            string format;
            if (showPercent)
            {
                format = string.Format("{0:0.0}%", playerHealth.HealthPercentage);
            }
            else
            {
                format = string.Format("{0:0}/{1:0}", playerHealth.HealthPoints, playerHealth.MaxHealthPoints); 
            }
            healthValueText.text = format;
        }

        #endregion

    }
}