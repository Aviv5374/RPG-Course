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

        object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (MySaveableEntity saveable in FindObjectsOfType<MySaveableEntity>())
            {
                state[saveable.UniqueIdentifier] = saveable.CaptureState();
            }
            return state;
        }

        void RestoreState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            foreach (MySaveableEntity saveable in FindObjectsOfType<MySaveableEntity>())
            {
                saveable.RestoreState(stateDict[saveable.UniqueIdentifier]);
            }
        }

        public void Save(string saveFileName)
        {
            string path = GetPathFromSaveFile(saveFileName);
            Debug.Log("Saving to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create)) 
            {                               
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, CaptureState());                
            }            
        }

        public void Load(string saveFileName)
        {
            string path = GetPathFromSaveFile(saveFileName);
            Debug.Log("Loading from " + path);
            using (FileStream stream = File.Open(path, FileMode.Open))
            {                
                BinaryFormatter formatter = new BinaryFormatter();
                RestoreState(formatter.Deserialize(stream));
            }

        }
        
        //public Object LoadLastScene(string saveFileName)
        //{
        //    return null;
        //}

        //public void Delete(string saveFileName)
        //{

        //}
    }
}
