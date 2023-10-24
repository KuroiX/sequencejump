using System;
using UnityEngine;

namespace Features.Player.Controller.CharacterInput
{
    public class QueueInput
    {
        private readonly QueueProcessor _queueProcessor;
        private readonly ICharacterInput[] _characterInput;

        private readonly IInputSetter _controllerInput;
        private bool _isDisabled;
        private float _savedMovementInput;
        private readonly IStopStartSignal[] _signals;
        
        public QueueInput(QueueProcessor queueProcessor, ICharacterInput[] inputManager, IInputSetter controllerInput, IStopStartSignal[] iSignals)
        {
            _characterInput = inputManager;
            _controllerInput = controllerInput;
            _queueProcessor = queueProcessor;
            _signals = iSignals;
        }
        
        public void HandleOnEnable()
        {
            foreach (ICharacterInput inputSource in _characterInput)
            {
                inputSource.ActionPerformed += Action;
                inputSource.ActionCanceled += ActionEnd;
            
                inputSource.MovePerformed += Move;
                inputSource.MoveCanceled += Move;
            }
            
            foreach (var signal in _signals)
            {
                signal.Stop += DisableInput;
                signal.Start += EnableInput;
            }
        }
        
        public void HandleOnDisable()
        {
            foreach (ICharacterInput inputSource in _characterInput)
            {
                inputSource.ActionPerformed -= Action;
                inputSource.ActionCanceled -= ActionEnd;
            
                inputSource.MovePerformed -= Move;
                inputSource.MoveCanceled -= Move;
            }
            
            foreach (var signal in _signals)
            {
                signal.Stop -= DisableInput;
                signal.Start -= EnableInput;
            }
        }
        
        private void Move(float value)
        {
            _savedMovementInput = value;

            if (_isDisabled) return;
            
            _controllerInput.SetHorizontal(value);
        }

        private void Action()
        {
            if(_isDisabled) return;
            _queueProcessor.PerformAction(_controllerInput);
        }

        private void ActionEnd()
        {
            if(_isDisabled) return;
            _queueProcessor.CancelAction(_controllerInput);
        }

        public void HandleLateUpdate()
        {
            _controllerInput.Reset();
        }
        
        private void DisableInput(object sender, EventArgs args)
        {
            _isDisabled = true;
        }
        
        private void EnableInput(object sender, EventArgs args)
        {
            _isDisabled = false; 
            _controllerInput.SetHorizontal(_savedMovementInput);
        }
    }
}