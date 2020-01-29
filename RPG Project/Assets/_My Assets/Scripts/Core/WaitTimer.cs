using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class WaitTimer : MonoBehaviour
    {
        [SerializeField] float minWaitTime = 3.5f;

        float timeSinceTriger = Mathf.Infinity;

        public bool IsWaitingTimeOver { get { return timeSinceTriger < minWaitTime; } }

        void Start()
        {
            //??????
        }

        void Update()
        {
            timeSinceTriger += Time.deltaTime;
        }

        public void ResetTimer()
        {
            timeSinceTriger = 0;
        }

    }
}