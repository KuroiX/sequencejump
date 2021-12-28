using System;
using UnityEngine;

namespace Features.Player.DeathLogic
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
            DeathLogicBehaviour.DeathAnimationStart += PlayEffect;
        }

        private void OnDisable()
        {
            DeathLogicBehaviour.DeathAnimationStart -= PlayEffect;
        }

        private void PlayEffect(object sender, EventArgs args)
        {
            _particleSystem.Play();
        }
    }
}
