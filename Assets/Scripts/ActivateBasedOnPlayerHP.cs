using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ActivateBasedOnPlayerHP : MonoBehaviour
    {
        [Range(0f,1f)]
        public float Threshold;
        public bool ActiveWhenAbovethreshold;

        private void Start()
        {
            PlayerHealth.Instance.HealthPercent += Foo;
        }

        private void Foo(float obj)
        {
            if (ActiveWhenAbovethreshold)
            {
                if (obj > Threshold)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                if (obj > Threshold)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
            }
        }
    }
}