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

        public void Save(string saveFileName)
        {
            string path = GetPathFromSaveFile(saveFileName);
            Debug.Log("Saving to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create)) 
            {                
                MySerializableVector3 position = new MySerializableVector3(GetPlayerTransform().position);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, position);
                //|
                //V
                //byte[] buffer = SerializeVector(playerTransform.position);
                //stream.Write(buffer, 0, buffer.Length);
            }            
        }

        public void Load(string saveFileName)
        {
            string path = GetPathFromSaveFile(saveFileName);
            Debug.Log("Loading from " + path);
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                Transform playerTransform = GetPlayerTransform();
                BinaryFormatter formatter = new BinaryFormatter();
                MySerializableVector3 position = (MySerializableVector3)formatter.Deserialize(stream);
                playerTransform.position = position.ToVector();
                //|
                //V
                //byte[] buffer = new byte[stream.Length];
                //stream.Read(buffer, 0, buffer.Length);
                //playerTransform.position = DeserializeVector(buffer);
            }

        }

        private Transform GetPlayerTransform()
        {
            return GameObject.FindWithTag("Player").transform;
        }

        private byte[] SerializeVector(Vector3 vector)
        {
            byte[] vectorBytes = new byte[3 * 4];
            BitConverter.GetBytes(vector.x).CopyTo(vectorBytes, 0);
            BitConverter.GetBytes(vector.y).CopyTo(vectorBytes, 4);
            BitConverter.GetBytes(vector.z).CopyTo(vectorBytes, 8);
            return vectorBytes;
        }

        private Vector3 DeserializeVector(byte[] buffer)
        {
            Vector3 result = new Vector3();
            result.x = BitConverter.ToSingle(buffer, 0);
            result.y = BitConverter.ToSingle(buffer, 4);
            result.z = BitConverter.ToSingle(buffer, 8);
            return result;
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
