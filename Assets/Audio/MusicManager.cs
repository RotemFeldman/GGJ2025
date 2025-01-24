using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class MusicManager : MonoBehaviour
    {
        private EventInstance musicInstance;
        [SerializeField] PlayerHealth playerHealth;

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