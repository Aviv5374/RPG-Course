using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Control;

namespace RPG.Resources
{
    public class XPDisplay : MonoBehaviour
    {

        [SerializeField] Text experienceValueText = null;

        Experience playerXP;

        #region Initialization

        void Awake()
        {

        }

        void Start()
        {
            playerXP = FindObjectOfType<PlayerController>().GetComponent<Experience>();
        }

        #endregion

        #region Updating	 

        void Update()
        {
            experienceValueText.text = string.Format("{0:0}", playerXP.ExperiencePoints);
        }

        #endregion

    }
}