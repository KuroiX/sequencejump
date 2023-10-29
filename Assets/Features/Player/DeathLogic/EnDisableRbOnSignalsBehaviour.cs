using Core;
using UnityEngine;

namespace Features.Player.DeathLogic
{
    public class EnDisableRbOnSignalsBehaviour : MonoBehaviour
    {
        [SerializeField] private TimedSignalBehaviour[] signals;
        [SerializeField] private Rigidbody2D rb;
        
        private void Awake()
        {
            rb = rb ? rb : GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            foreach (var signal in signals)
            {
                signal.Started += Disable;
                signal.Stopped += Enable;
            }
        }

        private void OnDisable()
        {
            foreach (var signal in signals)
            {
                signal.Started -= Disable;
                signal.Stopped -= Enable;
            }
        }

        private void Disable()
        {
            rb.simulated = false;
        }

        private void Enable()
        {
            rb.simulated = true;
        }
    }
}