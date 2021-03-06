﻿using System;
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
        [SerializeField] bool shouldUseModifiers = false;

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

        public Experience Experience 
        {
            get 
            {
                if (!experience)
                {
                    experience = GetComponent<Experience>();
                }

                return experience;
            }
            
            private set => experience = value; 
        }

        #region Initialization

        void Awake()
        {
            Experience = GetComponent<Experience>();
        }

        void OnEnable()
        {
            if (Experience != null)
            {
                Experience.onExperienceGained += UpdateLevel;
            }
        }

        void Start()
        {
            CurrentLevel = CalculateLevel();
        }

        #endregion

        void OnDisable()
        {
            if (Experience != null)
            {
                Experience.onExperienceGained -= UpdateLevel;
            }
        }

        public float GetStat(Stat stat)
        {
                                                                 //????????????????????????????????????????
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (1 + GetPercentageModifier(stat) / 100);
        }

        float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, CurrentLevel);
        }

        float GetAdditiveModifier(Stat stat)
        {
            float total = 0;
            if (shouldUseModifiers)
            {
                foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
                {
                    foreach (float modifier in provider.GetAdditiveModifiers(stat))
                    {
                        total += modifier;
                    }
                }
            }
            return total;
        }

        float GetPercentageModifier(Stat stat)//???????
        {
            float total = 0;
            if (shouldUseModifiers)
            {
                foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
                {
                    foreach (float modifier in provider.GetPercentageModifiers(stat))
                    {
                        total += modifier;
                    }
                }
            }
            
            return total;
        }

        int CalculateLevel()
        {
            if (Experience == null) { return startingLevel; }

            float currentXP = Experience.ExperiencePoints;
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