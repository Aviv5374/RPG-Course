﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.My.Saving
{
    public class MySavingSystem : MonoBehaviour
    {
        const string lastSceneBuildIndexKey = "lastSceneBuildIndex";

        string GetPathFromSaveFile(string saveFileName)
        {
            return Path.Combine(Application.persistentDataPath, saveFileName + ".sav");
        }

        #region Saving

        public void Save(string saveFileName)
        {
            Dictionary<string, object> state = LoadFile(saveFileName);
            CaptureState(state);
            SaveFile(saveFileName, state);
        }

        void SaveFile(string saveFileName, Dictionary<string, object> state)
        {
            string path = GetPathFromSaveFile(saveFileName);
            Debug.Log("Saving to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        void CaptureState(Dictionary<string, object> state)
        {            
            foreach (MySaveableEntity saveable in FindObjectsOfType<MySaveableEntity>())
            {
                state[saveable.UniqueIdentifier] = saveable.CaptureState();
            }

            //TODO: Make work through SceneLoaderManager
            state[lastSceneBuildIndexKey] = SceneManager.GetActiveScene().buildIndex;
        }

        #endregion

        #region Loading

        public void Load(string saveFileName)
        {            
            RestoreState(LoadFile(saveFileName));
        }

        public IEnumerator LoadLastScene(string saveFileName)
        {
            Dictionary<string, object> state = LoadFile(saveFileName);
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            if (state.ContainsKey(lastSceneBuildIndexKey))
            {
                buildIndex = (int)state[lastSceneBuildIndexKey];                
            }
            yield return SceneManager.LoadSceneAsync(buildIndex);
            RestoreState(state);
        }

        Dictionary<string, object> LoadFile(string saveFileName)
        {
            string path = GetPathFromSaveFile(saveFileName);
            if (!File.Exists(path))
            {
                return new Dictionary<string, object>();
            } 
            Debug.Log("Loading from " + path);
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        void RestoreState(Dictionary<string, object> state)
        {            
            foreach (MySaveableEntity saveable in FindObjectsOfType<MySaveableEntity>())
            {
                string ID = saveable.UniqueIdentifier;
                if (state.ContainsKey(ID))
                {
                    saveable.RestoreState(state[ID]);
                }
            }
        }

        #endregion

        public void Delete(string saveFileName)
        {
            string path = GetPathFromSaveFile(saveFileName);
            if (File.Exists(path))
            {
                Debug.Log("Deleting " + saveFileName + " from " + path);
                File.Delete(path);
            }
        }
    }
}
