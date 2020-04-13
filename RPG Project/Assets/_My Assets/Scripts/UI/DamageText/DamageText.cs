using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.DamageText
{
    public class DamageText : MonoBehaviour
    {
        Text damageText;

        #region Initialization

        void Awake()
        {
            damageText = GetComponentInChildren<Text>();
        }

        void Start()
        {

        }

        #endregion

        public void SetTextValue(float amount)
        {
            damageText.text = string.Format("{0:0}", amount);
        }

        #region Updating	 

        void Update()
        {

        }

        #endregion

    }
}