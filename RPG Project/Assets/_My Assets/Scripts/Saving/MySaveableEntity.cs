using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.My.Saving
{
    [ExecuteAlways]
    public class MySaveableEntity : MonoBehaviour
    {
        [SerializeField] string uniqueIdentifier = "";
        
        public string UniqueIdentifier { get { return uniqueIdentifier; } }        
        bool isInPlayMode { get { return Application.IsPlaying(gameObject); } }
        bool isInPrefabScene { get { return string.IsNullOrEmpty(gameObject.scene.path); } }
        
        void Update()
        {            
            if (isInPlayMode || isInPrefabScene) { return; }

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("uniqueIdentifier");
            if (string.IsNullOrEmpty(serializedProperty.stringValue))
            {
                serializedProperty.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }            
        }

        public object CaptureState()
        {
            print("Capturing state for " + UniqueIdentifier);
            return new MySerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            print("Restoring state for " + UniqueIdentifier);
            MySerializableVector3 POS = (MySerializableVector3)state;
            GetComponent<NavMeshAgent>().Warp(POS.ToVector());
        }
    }
}
