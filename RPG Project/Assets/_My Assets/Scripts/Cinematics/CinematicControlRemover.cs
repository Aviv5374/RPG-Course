using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Control;

namespace RPG.Cinematics
{
    [RequireComponent(typeof(PlayableDirector))]
    public class CinematicControlRemover : MonoBehaviour
    {
        PlayableDirector myPlayableDirector;
        PlayerController player;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<CinematicTrigger>().eventTest += EnableControl;

            myPlayableDirector = GetComponent<PlayableDirector>();
            myPlayableDirector.played += DisableControl;
            myPlayableDirector.stopped += EnableControl;

            player = FindObjectOfType<PlayerController>();
        }

        void DisableControl(PlayableDirector pd)
        {
            print("DisableControl");
            player.StopMoving();//OR player.CancelCurrentAction();
            player.CancelAttack();
            player.enabled = false;
        }

        void EnableControl(PlayableDirector pd)
        {
            print("EnableControl");
            player.enabled = true;
        }

        void EnableControl(float nonsese)
        {
            print("EnableControl");
            player.enabled = true;
        }
    }
}
