using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.My.Saving;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, IMySaveable
    {
        [SerializeField] float experiencePoints = 0;

        //public delegate void ExperienceGainedDelegate();
        //public event ExperienceGainedDelegate onExperienceGained;
        //|
        //V
        public event Action onExperienceGained;

        #region Test Delegates

        //public delegate void ExperienceGainedDelegate2(int num);
        //public event ExperienceGainedDelegate2 onTestDelegate2;

        ////public delegate bool ExperienceGainedDelegate2(int num ,float mun2);
        //public delegate bool ExperienceGainedDelegate3(int num, float mun2);
        //public event ExperienceGainedDelegate3 onTestDelegate3;

        #endregion

        public float ExperiencePoints { get => experiencePoints; }

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            onExperienceGained();
        }        
       
        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }

    }
}