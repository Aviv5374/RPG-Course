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

        Health playerHealth;

        #region Initialization

        void Awake()
        {

        }

        void Start()
        {
            playerHealth = FindObjectOfType<PlayerController>().GetComponent<Health>();
        }

        #endregion

        #region Updating	 

        void Update()
        {
            healthValueText.text = string.Format("{0:0.0}%", playerHealth.HealthPercentage.ToString());
        }

        #endregion

    }
}