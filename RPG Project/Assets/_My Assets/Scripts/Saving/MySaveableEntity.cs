﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RPG.My.Saving
{
    [ExecuteAlways]
    public class MySaveableEntity : MonoBehaviour
    {
        [SerializeField] string uniqueIdentifier = "";
        public string UniqueIdentifier 
        {
            get 
            {
                return uniqueIdentifier;
            }            
        }

        bool isInPlayMode { get { return Application.IsPlaying(gameObject); } }
        bool isInPrefabScene { get { return string.IsNullOrEmpty(gameObject.scene.path); } }

        public string GetUniqueIdentifier()
        {
            return "";
        }

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
            print("Capturing state for " + GetUniqueIdentifier());
            return null;
        }

        public void RestoreState(object state)
        {
            print("Restoring state for " + GetUniqueIdentifier());
        }
    }
}
