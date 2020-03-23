using System.IO;
using System.Text;
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
            string path = GetPathFromSaveFile(saveFileName);
            Debug.Log("Saving to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create)) 
            { 
                stream.WriteByte(102);
                byte[] bytes = Encoding.UTF8.GetBytes("¡Hola Mundo!");
                stream.Write(bytes, 0, bytes.Length);
            }            
        }

        public void Load(string saveFileName)
        {
            string path = GetPathFromSaveFile(saveFileName);
            Debug.Log("Loading from " + path);
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                Debug.Log(Encoding.UTF8.GetString(buffer));
            }
            
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
