using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectsSpawner : MonoBehaviour
    {
        static bool hasSpawned = false;

        [SerializeField] PersistentObjectsPrefab persistentObjectPrefab;

        void Awake()
        {
            if (hasSpawned) return;

            SpawnPersistentObjects();

            hasSpawned = true;
        }

        void SpawnPersistentObjects()
        {
            PersistentObjectsPrefab persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
