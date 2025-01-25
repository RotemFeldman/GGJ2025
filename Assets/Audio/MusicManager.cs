using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance;

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

        public EventInstance musicInstance;
        [SerializeField] public PlayerHealth playerHealth;

        private void Start()
        {
            musicInstance = AudioManager.Instance.CreateInstance(FmodEvents.Instance.Music);
            musicInstance.start();
        }
        
        

        private void Update()
        {
            musicInstance.setParameterByName("PlayerHp",Mathf.Clamp01(playerHealth.AirT));
        }
    }
}