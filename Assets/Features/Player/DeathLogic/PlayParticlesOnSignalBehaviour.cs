using Core;
using UnityEngine;

namespace Features.Player.DeathLogic
{
    public class PlayParticlesOnSignalBehaviour : MonoBehaviour
    {
        [SerializeField] private TimedSignalBehaviour signal;
        [SerializeField] private ParticleSystem particles;

        private void Awake()
        {
            particles = particles ? particles : GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        {
            signal.Started += PlayEffect;
        }

        private void OnDisable()
        {
            signal.Started -= PlayEffect;
        }

        private void PlayEffect()
        {
            particles.Play();
        }
    }
}
