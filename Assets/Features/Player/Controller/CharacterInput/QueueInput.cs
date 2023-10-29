using System;
using Core;
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
        private readonly TimedSignalBehaviour[] _signals;
        
        public QueueInput(QueueProcessor queueProcessor, ICharacterInput[] inputManager, IInputSetter controllerInput, TimedSignalBehaviour[] iSignals)
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
                signal.Started += DisableInput;
                signal.Stopped += EnableInput;
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
                signal.Started -= DisableInput;
                signal.Stopped -= EnableInput;
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
        
        private void DisableInput()
        {
            _isDisabled = true;
        }
        
        private void EnableInput()
        {
            _isDisabled = false; 
            _controllerInput.SetHorizontal(_savedMovementInput);
        }
    }
}