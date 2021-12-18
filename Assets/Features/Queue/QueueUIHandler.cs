using System;
using Features.Actions;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Queue
{
    public class QueueUIHandler : MonoBehaviour
    {
        [SerializeField] private Image first;
        [SerializeField] private Image second;
        [SerializeField] private Image third;
        [SerializeField] private Image forth;

        private void OnItemEnqueued(object sender, EventArgs args)
        {
            
        }
        
        private void OnItemDequeued(object sender, EventArgs args)
        {
            first.sprite = second.sprite;
            second.sprite = third.sprite;
            third.sprite = forth.sprite;
            
            ResettableQueue<ICharacterAction> queue = ((QueueEventArgs<ICharacterAction>)args).Queue;

            ICharacterAction action = queue.Peek(3);

            forth.sprite = action.Sprite;
        }
        
        private void OnQueueReset(object sender, EventArgs args)
        {
            
        }
        
        private void OnQueueCleared(object sender, EventArgs args)
        {
            
        }
    }
}