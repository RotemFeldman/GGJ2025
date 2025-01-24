using System;
using FMOD.Studio;
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
        
        [Header("Music")]
        public EventReference Music;
        
        [Header("Player")]
        public EventReference PlayerWalkLoop;
        

        /*void Foo()
        {
            var ins = RuntimeManager.CreateInstance(Music);
            ins.setParameterByName("")
        }*/
    }
}