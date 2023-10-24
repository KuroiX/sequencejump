using System;
using UnityEngine;

namespace Features.Player
{
    public class InputSourceHandler
    {
        private readonly ICharacterInput[] _charInput;

        private readonly IStopStartSignal[] _signals;

        public InputSourceHandler(ICharacterInput[] charInput, IStopStartSignal[] signals)
        {
            _charInput = charInput;
            _signals = signals;
        }
        
        public void HandleOnEnable()
        {
            foreach (var signal in _signals)
            {
                Debug.Log("hallo??");
                signal.Stop += DisableInput;
                signal.Start += EnableInput;
            }
        }

        public void HandleOnDisable()
        {
            foreach (var signal in _signals)
            {
                signal.Stop -= DisableInput;
                signal.Start -= EnableInput;
            }
        }
        
        private void DisableInput(object sender, EventArgs args)
        {
            foreach (var input in _charInput)
            {
                input.Disable();
            }
        }
        
        private void EnableInput(object sender, EventArgs args)
        {
            foreach (var input in _charInput)
            {
                input.Enable();
            }
        }
    }
}