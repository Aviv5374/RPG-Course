using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RPG.My.Saving
{
    [ExecuteAlways]
    public class MySaveableEntity : MonoBehaviour
    {
        static Dictionary<string, MySaveableEntity> globalLookup = new Dictionary<string, MySaveableEntity>();

        [SerializeField] string uniqueIdentifier = "";
        
        public string UniqueIdentifier { get { return uniqueIdentifier; } }        
        bool isInPlayMode { get { return Application.IsPlaying(gameObject); } }
        bool isInPrefabScene { get { return string.IsNullOrEmpty(gameObject.scene.path); } }

#if UNITY_EDITOR
        void Update()
        {            
            if (isInPlayMode || isInPrefabScene) { return; }

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serialProperty = serializedObject.FindProperty("uniqueIdentifier");
            if (string.IsNullOrEmpty(serialProperty.stringValue) || !IsUnique(serialProperty.stringValue))
            {
                serialProperty.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
            globalLookup[serialProperty.stringValue] = this;
        }
#endif

        bool IsUnique(string candidate)
        {
            if (!globalLookup.ContainsKey(candidate) || globalLookup[candidate] == this) return true;
            
            if (globalLookup[candidate] == null || globalLookup[candidate].uniqueIdentifier != candidate)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            return false;
        }

        public object CaptureState()
        {
            //print("Capturing state for " + uniqueIdentifier);
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (IMySaveable saveable in GetComponents<IMySaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;
        }

        public void RestoreState(object state)
        {
            //print("Restoring state for " + uniqueIdentifier);
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            foreach (IMySaveable saveable in GetComponents<IMySaveable>())
            {
                string typeString = saveable.GetType().ToString();
                if (stateDict.ContainsKey(typeString))
                {
                    saveable.RestoreState(stateDict[typeString]);
                }
            }
        }
    }
}
