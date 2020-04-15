using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Control;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D
        }

        [SerializeField] int sceneToLoad;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 0.5f;


        void OnTriggerEnter(Collider other)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player)
            {
                StartCoroutine(Transition());
            }
        }

        IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set!");
                yield break;
            }

            DontDestroyOnLoad(gameObject);
            Fader fader = FindObjectOfType<Fader>();
            PlayerController player = FindObjectOfType<PlayerController>();
            player.enabled = false;

            yield return fader.FadeOut(fadeOutTime);

            MySavingWrapper wrapper = FindObjectOfType<MySavingWrapper>();
            wrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            PlayerController newPlayer = FindObjectOfType<PlayerController>();
            newPlayer.enabled = false;

            wrapper.Load();

            SetupLevel();

            yield return new WaitForSeconds(fadeWaitTime);
            wrapper.ResetPlayerCamera();
            wrapper.Save();
            fader.FadeIn(fadeInTime);
            newPlayer.enabled = true;

            Destroy(gameObject);
        }

        void SetupLevel()
        {
            Portal otherPortal = GetOtherPortal();
            //TestPortal(otherPortal);
            UpdatePlayer(otherPortal);
            Debug.Log("Scene Loaded From " + name);
        }

        void TestPortal(Portal otherPortal)
        {
            if (!otherPortal)
            {
                Debug.LogError("otherPortal is NULL");
            }
            else if (!otherPortal.spawnPoint)
            {
                Debug.LogError("otherPortal.spawnPoint is NULL");
            }
            else
            {
                Debug.Log(otherPortal.spawnPoint.position.ToString());
            }
        }

        Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination == this.destination)
                {                    
                    return portal;
                }
            }

            return null;
        }

        void UpdatePlayer(Portal otherPortal)
        {
            PlayerController player = FindObjectOfType<PlayerController>();            
            NavMeshAgent playerAgent = player.GetComponent<NavMeshAgent>();           
            playerAgent.Warp(otherPortal.spawnPoint.position);           
            player.transform.rotation = otherPortal.spawnPoint.rotation;            
        }
    }
}
