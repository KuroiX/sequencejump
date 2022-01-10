using System;
using UnityEngine;

namespace Features.Player.DeathLogic
{
    public class PlayParticlesOnSignalBehaviour : MonoBehaviour
    {
        [SerializeField] private Component signal;
        [SerializeField] private ParticleSystem particles;

        private IStopSignal _signal;

        private void Awake()
        {
            _signal = (IStopSignal) signal;
            particles = particles ? particles : GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        {
            _signal.Stop += PlayEffect;
        }

        private void OnDisable()
        {
            _signal.Stop -= PlayEffect;
        }

        private void PlayEffect(object sender, EventArgs args)
        {
            particles.Play();
        }
    }
}
