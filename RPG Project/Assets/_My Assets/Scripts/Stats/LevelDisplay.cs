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
            playerBaseStats = FindObjectOfType<PlayerController>().GetComponent<BaseStats>();
            //levelhValueText = GetComponent<Text>();//?????
        }

        void Start()
        {
        }

        #endregion

        #region Updating	 

        void Update()
        {
            levelhValueText.text = string.Format("{0:0}", playerBaseStats.CurrentLevel);
        }

        #endregion

    }
}