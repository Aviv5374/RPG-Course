using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent = null;
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas rootCanvas = null;


        bool IsHealtBarFull { get { return Mathf.Approximately(healthComponent.HealFraction, 1); } }
        bool IsHealtBarEmpty { get { return Mathf.Approximately(healthComponent.HealFraction, 0); } }

        #region Updating	 

        void Update()
        {
            if (IsHealtBarFull || IsHealtBarEmpty)
            {
                rootCanvas.enabled = false;
                return;
            }

            foreground.localScale = new Vector3(healthComponent.HealFraction, 1, 1);
        }

        #endregion

    }
}