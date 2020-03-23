using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.My.Saving
{
    [ExecuteAlways]
    public class MySaveableEntity : MonoBehaviour
    {
        [SerializeField] string uniqueIdentifier = ""; //System.Guid.NewGuid().ToString();

        public string UniqueIdentifier 
        {
            get 
            {
                return uniqueIdentifier;
            }

            private set 
            {
                
            }
        }

        public string GetUniqueIdentifier()
        {
            return "";
        }

        void Update()
        {
            if (Application.IsPlaying(gameObject)) { return; }
            Debug.Log("Editing");
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
