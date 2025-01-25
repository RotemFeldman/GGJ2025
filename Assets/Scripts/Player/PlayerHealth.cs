using System;
using System.Collections;
using Audio;
using DG.Tweening;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour
{
    
    public bool IsStealth;
    [SerializeField] private GameObject _playerVisual;

    [SerializeField] private float maxAir = 100f;
    [SerializeField] private float currentAir;
    [SerializeField] private float decayRate = 1f;

    [FormerlySerializedAs("_airT")] public float AirT;
    
    [SerializeField] Vector3 maxScale = Vector3.one;
    [SerializeField] Vector3 minScale = Vector3.zero;
    
    private EventInstance musicInstance;

    private void OnEnable()
    {
        currentAir = maxAir;
        AirT = 1;
        MusicManager.Instance.playerHealth = this;
    }

    private void Update()
    {
        currentAir -= decayRate * Time.deltaTime;
        if (currentAir < 0)
        {
            currentAir = 0;
            StartCoroutine(StartPlayerDeath());
        }
        
        AirT =  currentAir / maxAir;
        
        Vector3 newScale = Vector3.Lerp(minScale, maxScale, AirT);
        _playerVisual.transform.localScale = newScale;
    }

    private IEnumerator StartPlayerDeath()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GoodBubble"))
        {
            var gains = other.gameObject.GetComponent<GoodBubble>().AirAmount;
            currentAir += gains;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("BadBubble"))
        {
            var lose = other.gameObject.GetComponent<BadBubble>().Damage;
            currentAir -= lose;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("StealthBubble"))
        {
            var bubble = other.gameObject.GetComponent<StealthBubble>();
            bubble.OnPopped += StealthBubblePopped;
            IsStealth = true;
            bubble.transform.DOMove(transform.position, 0.1f);
            bubble.Activate(transform);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Base"))
        {
            currentAir += (10 + decayRate) * Time.deltaTime;
            if(currentAir >= maxAir)
                currentAir = maxAir;
        }
    }


    void StealthBubblePopped()
    {
        IsStealth = false;
    }
}
