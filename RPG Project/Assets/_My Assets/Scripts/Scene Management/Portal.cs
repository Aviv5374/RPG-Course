using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Control;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad;
        
        void OnTriggerEnter(Collider other)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player)
            {
                StartCoroutine(Transition());
            }
        }

        IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            AsyncOperation asyncOperationScene = SceneManager.LoadSceneAsync(sceneToLoad);            
            yield return asyncOperationScene;
            Debug.Log("Scene Loaded From " + name);
            //yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }
    }
}
