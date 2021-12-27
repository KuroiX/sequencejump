using System;
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

        public void OnLeft(float value)
        {
            LeftEvent?.Invoke(this, new WorkaroundEventArgs(value));
        }

        public void OnRight(float value)
        {
            RightEvent?.Invoke(this, new WorkaroundEventArgs(value));
        }

        public void OnActionDown()
        {
            ActionDownEvent?.Invoke(this, new EventArgs());
        }

        public void OnActionUp()
        {
            ActionUpEvent?.Invoke(this, new EventArgs());
        }
    }
}
