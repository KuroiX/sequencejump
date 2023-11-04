using System;
using Core.Actions;
using Core.Queue;
using Features.StationLogic;
using UnityEngine;

namespace Features.QueueCycle
{
    public class QueueCycleBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject buttonObject;
        
        private ResettableQueue<ICharacterAction> _queue;

        private void Start()
        {
            _queue = FindObjectOfType<QueueHolder>().Queue;
        }

        private void OnEnable()
        {
            Station.StationEntered += StationEnteredBehaviour;
            Station.StationExited += StationExitedBehaviour;
        }

        private void OnDisable()
        {
            Station.StationEntered -= StationEnteredBehaviour;
            Station.StationExited -= StationExitedBehaviour;
        }

        private void StationEnteredBehaviour(object sender, EventArgs args)
        {
            buttonObject.SetActive(false);
        }

        private void StationExitedBehaviour(object sender, EventArgs args)
        {
            buttonObject.SetActive(true);
        }

        public void CycleQueue()
        {
            var action = _queue.Dequeue();
            _queue.Enqueue(action);
        }
    }
}