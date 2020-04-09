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
        CinematicTrigger cinematicTrigger;
        PlayableDirector myPlayableDirector;
        PlayerController player;

        void Awake()
        {
            cinematicTrigger = GetComponent<CinematicTrigger>();
            myPlayableDirector = GetComponent<PlayableDirector>();
            player = FindObjectOfType<PlayerController>();
        }

        void OnEnable()
        {
            cinematicTrigger.eventTest += EnableControl;

            myPlayableDirector.played += DisableControl;
            myPlayableDirector.stopped += EnableControl;
        }

        void Start()
        {            

        }

        void OnDisable()
        {
            cinematicTrigger.eventTest -= EnableControl;

            myPlayableDirector.played -= DisableControl;
            myPlayableDirector.stopped -= EnableControl;
        }

        void DisableControl(PlayableDirector pd)
        {
            Debug.Log("DisableControl");
            player.StopMoving();//OR player.CancelCurrentAction();
            player.CancelAttack();
            player.enabled = false;
        }

        void EnableControl(PlayableDirector pd)
        {
            Debug.Log("EnableControl");
            player.enabled = true;
        }

        void EnableControl(float nonsese)
        {
            Debug.Log("EnableControlTest");
            player.enabled = true;
        }
    }
}
