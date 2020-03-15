using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    [RequireComponent(typeof(PlayableDirector))]
    public class CinematicControlRemover : MonoBehaviour
    {
        PlayableDirector myPlayableDirector;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<CinematicTrigger>().eventTest += EnableControl;

            myPlayableDirector = GetComponent<PlayableDirector>();
            myPlayableDirector.played += DisableControl;
            myPlayableDirector.stopped += EnableControl;
        }

        void DisableControl(PlayableDirector pd)
        {
            print("DisableControl");
        }

        void EnableControl(PlayableDirector pd)
        {
            print("EnableControl");
        }

        void EnableControl(float nonsese)
        {
            print("EnableControl");
        }
    }
}
