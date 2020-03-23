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

        // Update is called once per frame
        void Update()
        {

        }

        public void Save(string saveFileName)
        {
            Debug.Log("Saving to " + saveFileName);
        }

        public void Load(string saveFileName)
        {
            Debug.Log("Loading from " + saveFileName);
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
