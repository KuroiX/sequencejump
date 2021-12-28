using System;
using Features.Player.DeathLogic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Features.Player.Controller
{
    public class ButtonWorkaround : MonoBehaviour
    {
        public event EventHandler LeftEvent;
        public event EventHandler RightEvent;
        public event EventHandler ActionDownEvent;
        public event EventHandler ActionUpEvent;

        private bool _isDisabled = false;

        public void OnLeft(float value)
        {
            if(_isDisabled && value != 0) return;
            LeftEvent?.Invoke(this, new FloatEventArgs(value));
        }

        public void OnRight(float value)
        {
            if(_isDisabled && value != 0) return;
            RightEvent?.Invoke(this, new FloatEventArgs(value));
        }

        public void OnActionDown()
        {
            if(_isDisabled) return;
            ActionDownEvent?.Invoke(this, new EventArgs());
        }

        public void OnActionUp()
        {
            if(_isDisabled) return;
            ActionUpEvent?.Invoke(this, new EventArgs());
        }
        
        private void DisableInput(object sender, EventArgs args)
        {
            _isDisabled = true;
        }
        
        private void EnableInput(object sender, EventArgs args)
        {
            Debug.Log("EnableInput");
            _isDisabled = false;
        }
        
        private void OnDisable()
        {
            DeathLogicBehaviour.DeathAnimationStart -= DisableInput;
            DeathLogicBehaviour.DeathAnimationEnd -= EnableInput;
        }
        
        private void OnEnable()
        {
            DeathLogicBehaviour.DeathAnimationStart += DisableInput;
            DeathLogicBehaviour.DeathAnimationEnd += EnableInput;
        }
    }
}
