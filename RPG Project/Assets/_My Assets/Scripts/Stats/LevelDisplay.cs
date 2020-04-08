using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Control;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        [SerializeField] Text levelhValueText = null;

        BaseStats playerBaseStats;

        #region Initialization

        void Awake()
        {

        }

        void Start()
        {
            playerBaseStats = FindObjectOfType<PlayerController>().GetComponent<BaseStats>();
        }

        #endregion

        #region Updating	 

        void Update()
        {
            levelhValueText.text = string.Format("{0:0}", playerBaseStats.GetLevel());
        }

        #endregion

    }
}