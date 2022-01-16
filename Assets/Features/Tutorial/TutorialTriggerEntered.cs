using System;
using UnityEngine;

namespace Features.Tutorial
{
    public class TutorialTriggerEntered : MonoBehaviour
    {
        public event EventHandler TriggerEntered;
        //public event EventHandler TriggerExited;

        private void OnTriggerEnter2D(Collider2D col)
        {
            TriggerEntered?.Invoke(this, EventArgs.Empty);
        }

        // private void OnTriggerExit2D(Collider2D other)
        // {
        //     TriggerExited?.Invoke(this, EventArgs.Empty);
        // }
    }
}
