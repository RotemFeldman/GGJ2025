using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        private List<EventInstance> m_EventInstances;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            m_EventInstances = new List<EventInstance>();
        }

        public void PlayOneShot(EventReference clip, Vector3 position)
        {
            RuntimeManager.PlayOneShot(clip, position);
        }

        public EventInstance CreateInstance(EventReference eventReference)
        {
            EventInstance instance = RuntimeManager.CreateInstance(eventReference);
            m_EventInstances.Add(instance);
            return instance;
        }
        
        public void FadeOutMusic(float duration)
        {
            StartCoroutine(FadeOutCoroutine(duration));
        }
        

        private System.Collections.IEnumerator FadeOutCoroutine(float duration)
        {
            float elapsed = 0f;
            float initialVolume = 1.0f;

            var eventInstance = MusicManager.Instance.musicInstance;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float currentVolume = Mathf.Lerp(initialVolume, 0.0f, elapsed / duration);
                eventInstance.setParameterByName("Volume", currentVolume);
                yield return null;
            }

            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.release();
        }

        private void CleanUp()
        {
            foreach (var instance in m_EventInstances)
            {
                instance.stop(STOP_MODE.IMMEDIATE);
                instance.release();
            }
        }

        private void OnDestroy()
        {
            CleanUp();
        }
    }
}
