using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class CameraFacing : MonoBehaviour
    {
        Camera mainCamera = null;
        
        void Awake()
        {
            mainCamera = Camera.main;
        }
        
        #region Updating	 

        void LateUpdate()
        {
            transform.forward = mainCamera.transform.forward;
        }

        #endregion

    }
}