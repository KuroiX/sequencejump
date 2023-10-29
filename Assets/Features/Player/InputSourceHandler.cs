using System;
using Core;
using UnityEngine;

namespace Features.Player
{
    public class InputSourceHandler
    {
        private readonly ICharacterInput[] _charInput;

        private readonly TimedSignalBehaviour[] _signals;

        public InputSourceHandler(ICharacterInput[] charInput, TimedSignalBehaviour[] signals)
        {
            _charInput = charInput;
            _signals = signals;
        }
        
        public void HandleOnEnable()
        {
            foreach (var signal in _signals)
            {
                Debug.Log("hallo??");
                signal.Stopped += DisableInput;
                signal.Started += EnableInput;
            }
        }

        public void HandleOnDisable()
        {
            foreach (var signal in _signals)
            {
                signal.Stopped -= DisableInput;
                signal.Started -= EnableInput;
            }
        }
        
        private void DisableInput()
        {
            foreach (var input in _charInput)
            {
                input.Disable();
            }
        }
        
        private void EnableInput()
        {
            foreach (var input in _charInput)
            {
                input.Enable();
            }
        }
    }
}