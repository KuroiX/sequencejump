using System;
using UnityEngine;

namespace Features.Player.DeathLogic
{
    public class EnDisableRbOnSignalsBehaviour : MonoBehaviour
    {
        [SerializeField] private Component signals;
        [SerializeField] private Rigidbody2D rb;

        private IStopStartSignal _signals;

        private void Awake()
        {
            _signals = (IStopStartSignal) signals;
            rb = rb ? rb : GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _signals.Stop += Disable;
            _signals.Start += Enable;
        }

        private void OnDisable()
        {
            _signals.Stop -= Disable;
            _signals.Start -= Enable;
        }

        private void Disable(object sender, EventArgs args)
        {
            rb.simulated = false;
        }

        private void Enable(object sender, EventArgs args)
        {
            rb.simulated = true;
        }

    }
}