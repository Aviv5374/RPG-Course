using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace RPG.My.Saving
{
    public class MySavingSystem : MonoBehaviour
    {                
        string GetPathFromSaveFile(string saveFileName)
        {
            return Path.Combine(Application.persistentDataPath, saveFileName + ".sav");
        }

        #region Saving

        public void Save(string saveFileName)
        {            
            SaveFile(saveFileName, CaptureState());
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

        Dictionary<string, object> CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (MySaveableEntity saveable in FindObjectsOfType<MySaveableEntity>())
            {
                state[saveable.UniqueIdentifier] = saveable.CaptureState();
            }
            return state;
        }

        #endregion

        #region Loading

        public void Load(string saveFileName)
        {            
            RestoreState(LoadFile(saveFileName));
        }

        Dictionary<string, object> LoadFile(string saveFileName)
        {
            string path = GetPathFromSaveFile(saveFileName);
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
                saveable.RestoreState(state[saveable.UniqueIdentifier]);
            }
        }

        //public Object LoadLastScene(string saveFileName)
        //{
        //    return null;
        //}

        #endregion

        //public void Delete(string saveFileName)
        //{

        //}
    }
}
