using System;
using UnityEngine;

namespace Features.Player.Controller
{
    public class WorkaroundEventArgs : EventArgs
    {
        private float _value;

        public float Value
        {
            get => _value;
            private set => this._value = value;
        }

        public WorkaroundEventArgs(float value)
        {
            Value = value;
        }
    }
}
