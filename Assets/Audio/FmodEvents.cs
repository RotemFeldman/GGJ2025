using System;
using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class FmodEvents : MonoBehaviour
    {
        public static FmodEvents Instance;

        private void Awake()
        {

            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        [Header("Player")]
        public EventReference PlayerWalkLoop;
    }
}