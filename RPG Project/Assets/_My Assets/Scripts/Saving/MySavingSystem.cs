using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.My.Saving
{
    public class MySavingSystem : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }
        
        string GetPathFromSaveFile(string saveFileName)
        {
            return Path.Combine(Application.persistentDataPath, saveFileName + ".sav");
        }

        public void Save(string saveFileName)
        {
            Debug.Log("Saving to " + GetPathFromSaveFile(saveFileName));
        }

        public void Load(string saveFileName)
        {
            Debug.Log("Loading from " + GetPathFromSaveFile(saveFileName));
        }

        public Object LoadLastScene(string saveFileName)
        {
            return null;
        }

        public void Delete(string saveFileName)
        {

        }
    }
}
