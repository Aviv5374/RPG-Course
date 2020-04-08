using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.My.Saving;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, IMySaveable
    {
        [SerializeField] float experiencePoints = 0;

        public float ExperiencePoints { get => experiencePoints; }

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
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