using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using RPG.My.Saving;

namespace RPG.SceneManagement
{
    public class MySavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "mySave";

        [SerializeField] float fadeInTime = 0.2f;

        CinemachineVirtualCamera virtualCamera;
       
        MySavingSystem mySavingSystem;

        MySavingSystem MySavingSystem 
        {
            get 
            {
                if (!mySavingSystem)
                {
                    mySavingSystem = GetComponent<MySavingSystem>();
                }

                return mySavingSystem;
            }  
        }

        void Awake()
        {
            StartCoroutine(LoadLastScene());
        }

        IEnumerator LoadLastScene()
        {
            yield return MySavingSystem.LoadLastScene(defaultSaveFile);
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return fader.FadeIn(fadeInTime);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
                ResetPlayerCamera();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Delete();
            }
        }

        public void Save()
        {
            MySavingSystem.Save(defaultSaveFile);
        }

        public void Load()
        {
            MySavingSystem.Load(defaultSaveFile);
        }

        public void Delete()
        {
            MySavingSystem.Delete(defaultSaveFile);
        }

        public void ResetPlayerCamera()
        {
            if (!virtualCamera)
            {
                virtualCamera = GameObject.FindGameObjectWithTag("Player Camera").GetComponent<CinemachineVirtualCamera>();
            }
            virtualCamera.enabled = false;
            virtualCamera.enabled = true;
        }
    }
}
