using System;
using Features.Actions;
using Features.Queue;
using UnityEngine;

namespace Features.Player.Controller.CharacterInput
{
    public class QueueProcessor
    {
        private readonly ResettableQueue<ICharacterAction> _actionQueue;
        
        private bool _lastActionWasJump;

        public QueueProcessor(ResettableQueue<ICharacterAction> actionQueue)
        {
            _actionQueue = actionQueue;
        }
        
        public void Action(ICharacterInput characterInput)
        {
            if (_actionQueue.Count == 0) return;
            
            ICharacterAction currentAction = _actionQueue.Dequeue();

            switch (currentAction.Name)
            {
                case "Jump":
                    Jump(characterInput);
                    break;
                case "Dash":
                    Dash(characterInput);
                    break;
                default:
                    throw new NotImplementedException(
                        "An Action was dequeued that has not been implemented in QueueProcessor.cs");
            }
        }

        private void Jump(ICharacterInput characterInput)
        {
            characterInput.JumpPerformed = true;
            //characterInput.JumpTimeStamp = Time.unscaledTime;
            _lastActionWasJump = true;
        }
        
        public void JumpEnd(ICharacterInput characterInput)
        {
            if (!_lastActionWasJump) return;
            characterInput.JumpCanceled = true;
            //characterInput.JumpEndTimeStamp = Time.unscaledTime;
        }

        private void Dash(ICharacterInput characterInput)
        {
            characterInput.DashPerformed = true;
        }
    }
}