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
            musicInstance = RuntimeManager.CreateInstance(FmodEvents.Instance.Music);
        }

        private void Update()
        {
            musicInstance.setParameterByName("PlayerHp",Mathf.Clamp01(playerHealth.AirT));
        }
    }
}