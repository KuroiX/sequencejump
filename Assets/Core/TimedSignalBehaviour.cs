using System;
using System.Collections;
using UnityEngine;

namespace Core
{
    public abstract class TimedSignalBehaviour : MonoBehaviour
    {
        public event Action Started;
        public event Action Stopped;

        [SerializeField] protected float timeToStop;

        protected bool isRunning;

        private void OnStartTriggered()
        {
            Started?.Invoke();
        }

        private void OnStopTriggered()
        {
            Stopped?.Invoke();
        }
        
        protected IEnumerator StartTriggered()
        {
            isRunning = true;
            OnStartTriggered();

            yield return new WaitForSeconds(timeToStop);

            isRunning = false;
            OnStopTriggered();
        }
    }
}