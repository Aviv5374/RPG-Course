using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        CinemachineVirtualCamera virtualCamera;

        SavingSystem savingSystem;

        void Start()
        {
            savingSystem = GetComponent<SavingSystem>();
            virtualCamera = GameObject.FindGameObjectWithTag("Player Camera").GetComponent<CinemachineVirtualCamera>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                savingSystem.Delete(defaultSaveFile);
            }
        }

        public void Save()
        {
            savingSystem.Save(defaultSaveFile);
        }

        public void Load()
        {
            savingSystem.Load(defaultSaveFile);
            virtualCamera.enabled = false;
            virtualCamera.enabled = true;
        }
    }

}
