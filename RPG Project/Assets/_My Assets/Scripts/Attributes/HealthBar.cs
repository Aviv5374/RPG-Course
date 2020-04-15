using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent = null;
        [SerializeField] RectTransform foreground = null;
        

        #region Updating	 

        void Update()
        {
            foreground.localScale = new Vector3(healthComponent.HealFraction, 1, 1);
        }

        #endregion

    }
}