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
        public EventReference BubbleHeal;
        public EventReference BubblePickup;
        public EventReference BubblePop;
        public EventReference InvisibleBubble;

        [Header("Enemies")] 
        public EventReference EnemyDetect;
        
        [Header("World")]
        public EventReference GateOpen;
        public EventReference TimeBeep;
        public EventReference WallMove;
        public EventReference Loss;
        public EventReference Victory;
        public EventReference PlaceObject;


        /*void Foo()
        {
            var ins = RuntimeManager.CreateInstance(Music);
            ins.setParameterByName("")
        }*/
    }
}