using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class CameraFacing : MonoBehaviour
    {

        Camera mainCamera = null;

        #region Initialization

        void Awake()
        {
            mainCamera = Camera.main;
        }

        void Start()
        {

        }

        #endregion

        #region Updating	 

        void Update()
        {
            transform.forward = mainCamera.transform.forward;
        }

        #endregion

    }
}