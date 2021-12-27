using System;
using UnityEngine;

namespace Features.Player
{
    public class ActivateParticles : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        {
            HazardTriggerEnter.DeathAnimationStart += PlayEffect;
        }

        private void OnDisable()
        {
            HazardTriggerEnter.DeathAnimationStart -= PlayEffect;
        }

        private void PlayEffect(object sender, EventArgs args)
        {
            _particleSystem.Play();
        }
    }
}
