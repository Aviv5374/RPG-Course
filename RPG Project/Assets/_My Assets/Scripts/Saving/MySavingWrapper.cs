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
       
        MySavingSystem savingSystem;

        MySavingSystem SavingSystem 
        {
            get 
            {
                if (!savingSystem)
                {
                    savingSystem = GetComponent<MySavingSystem>();
                }

                return savingSystem;
            }  
        }
        
        //IEnumerator Start()
        //{
        //    Fader fader = FindObjectOfType<Fader>();
        //    fader.FadeOutImmediate();
        //    yield return SavingSystem.LoadLastScene(defaultSaveFile);
        //    yield return fader.FadeIn(fadeInTime);
        //}

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

            //if (Input.GetKeyDown(KeyCode.D))
            //{
            //    SavingSystem.Delete(defaultSaveFile);
            //}
        }

        public void Save()
        {
            SavingSystem.Save(defaultSaveFile);
        }

        public void Load()
        {
            SavingSystem.Load(defaultSaveFile);
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
