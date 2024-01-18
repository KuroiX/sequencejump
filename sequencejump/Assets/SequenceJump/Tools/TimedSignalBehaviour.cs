using System;
using System.Collections;
using UnityEngine;

namespace SequenceJump.Tools
{
    public abstract class TimedSignalBehaviour : MonoBehaviour
    {
        public event Action Started;
        public event Action Stopped;
        
        [SerializeField] protected float timeToStop;

        protected bool isRunning;

        protected void OnStartTriggered()
        {
            Started?.Invoke();
        }

        protected void OnStopTriggered()
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