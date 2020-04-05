using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.My.Saving;

namespace RPG.Resources
{
    public class Experience : MonoBehaviour, IMySaveable
    {
        [SerializeField] float experiencePoints = 0;
        
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