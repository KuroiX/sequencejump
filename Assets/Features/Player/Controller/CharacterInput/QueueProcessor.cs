using System;
using UnityEngine;
using Foundations.Actions;
using Foundations.Queue;


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
        
        public void Action(IControllerInput controllerInput)
        {
            if (_actionQueue.Count == 0) return;
            
            ICharacterAction currentAction = _actionQueue.Dequeue();

            switch (currentAction.Name)
            {
                case "Jump":
                    Jump(controllerInput);
                    break;
                case "Dash":
                    Dash(controllerInput);
                    break;
                default:
                    throw new NotImplementedException(
                        "An Action was dequeued that has not been implemented in QueueProcessor.cs");
            }
        }

        private void Jump(IControllerInput controllerInput)
        {
            controllerInput.JumpPerformed = true;
            //characterInput.JumpTimeStamp = Time.unscaledTime;
            _lastActionWasJump = true;
        }
        
        public void JumpEnd(IControllerInput controllerInput)
        {
            if (!_lastActionWasJump) return;
            controllerInput.JumpCanceled = true;
            //characterInput.JumpEndTimeStamp = Time.unscaledTime;
        }

        private void Dash(IControllerInput controllerInput)
        {
            controllerInput.DashPerformed = true;
        }
    }
}