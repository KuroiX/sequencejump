using System;
using Features.Player.DeathLogic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Features.Player.Controller
{
    public class ButtonWorkaroundInput : MonoBehaviour, ICharacterInput
    {
        public event Action ActionPerformed;
        public event Action ActionCanceled;
        public event Action<float> MovePerformed;
        public event Action<float> MoveCanceled;

        private float _leftValue;
        private float _rightValue;

        private bool _isDisabled;

        public void OnLeft(float value)
        {
            _leftValue = value;
            OnMove();
        }

        public void OnRight(float value)
        {
            _rightValue = value;
            OnMove();
        }

        private void OnMove()
        {
            if (_isDisabled) return;
            
            if (_leftValue == 0 && _rightValue == 0)
            {
                MoveCanceled?.Invoke(0);
            }
            else
            {
                MovePerformed?.Invoke(_leftValue + _rightValue);
            }
        }

        public void OnActionDown()
        {
            if(_isDisabled) return;
            ActionPerformed?.Invoke();
        }

        public void OnActionUp()
        {
            if(_isDisabled) return;
            ActionCanceled?.Invoke();
        }
        
        public void Disable()
        {
            _isDisabled = true;
            ActionCanceled?.Invoke();
            MoveCanceled?.Invoke(0);
        }
        
        public void Enable()
        {
            _isDisabled = false;
            MovePerformed?.Invoke(_leftValue + _rightValue);
        }
    }
}
