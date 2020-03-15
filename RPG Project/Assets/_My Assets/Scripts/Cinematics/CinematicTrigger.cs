using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Control;

namespace RPG.Cinematics
{
    [RequireComponent(typeof(PlayableDirector))]
    public class CinematicTrigger : MonoBehaviour
    {
        [SerializeField] int maxTimeCinematicCanTrigger = 1;
        int sumTimeCinematicTrigger = 0;
        //OR
        bool alreadyTriggered = false;

        PlayableDirector myPlayableDirector;


        // Start is called before the first frame update
        void Start()
        {
            myPlayableDirector = GetComponent<PlayableDirector>();
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player && sumTimeCinematicTrigger < maxTimeCinematicCanTrigger && !alreadyTriggered)
            {
                sumTimeCinematicTrigger++;
                //OR
                alreadyTriggered = true;
                //Always
                myPlayableDirector.Play();
            }
        }
    }
}
