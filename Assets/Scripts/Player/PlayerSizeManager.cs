using System;
using UnityEngine;

public class PlayerSizeManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerVisual;

    [SerializeField] private float maxAir = 100f;
    [SerializeField] private float currentAir;
    [SerializeField] private float decayRate = 1f;

    private float _airT;
    
    [SerializeField] Vector3 maxScale = Vector3.one;
    [SerializeField] Vector3 minScale = Vector3.zero;

    private void Start()
    {
        currentAir = maxAir;
    }

    private void Update()
    {
        currentAir -= decayRate * Time.deltaTime;
        _airT = currentAir / maxAir;
        
        Vector3 newScale = Vector3.Lerp(minScale, maxScale, _airT);
        _playerVisual.transform.localScale = newScale;
    }
}
