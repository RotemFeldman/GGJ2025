using System;
using System.Collections;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class MainMenuMusic : MonoBehaviour
{
    [SerializeField] private EventReference music;

    private EventInstance _reference;

    private void Start()
    {
        _reference = RuntimeManager.CreateInstance(music);
        _reference.start();
    }

    private void OnDestroy()
    {
        _reference.stop(STOP_MODE.IMMEDIATE);
    }

    public void Play()
    {
        StartCoroutine(NextScene());
    }

    private IEnumerator NextScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        yield return null;
    }
}
