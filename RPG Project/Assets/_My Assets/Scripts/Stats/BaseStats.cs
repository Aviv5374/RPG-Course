using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;

        Experience experience = null;

        int currentLevel = 0;

        public event Action onLevelUp;

        public int CurrentLevel 
        {
            get 
            {
                if (currentLevel < 1)
                {
                    currentLevel = CalculateLevel();
                }
                return currentLevel;
            }
            
            private set
            {
                currentLevel = value;
            }
        }

        #region Initialization

        void Awake()
        {
            experience = GetComponent<Experience>();
        }

        void Start()
        {                       
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        #endregion
            
        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, CurrentLevel);
        }

        public int CalculateLevel()
        {
            if (experience == null) { return startingLevel; }

            float currentXP = experience.ExperiencePoints;
            int penultimateLevel = progression.GetLevelsLength(Stat.ExperienceToLevelUp, characterClass);
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if (XPToLevelUp > currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel +1;
        }

        void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > CurrentLevel)
            {
                CurrentLevel = newLevel;
                //Debug.Log("Levelled Up!");
                LevelUpEffect();
                onLevelUp();
            }
        }

        void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }

        #region Updating	 

        void Update()
        {
            
        }

        #endregion

    }
}