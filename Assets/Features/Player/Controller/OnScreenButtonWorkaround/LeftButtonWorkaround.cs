using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Features.Player.Controller
{
    public class LeftButtonWorkaround : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private ButtonWorkaroundInput _buttonWorkaroundInput;

        private void Awake()
        {
            _buttonWorkaroundInput = gameObject.GetComponentInParent<ButtonWorkaroundInput>();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _buttonWorkaroundInput.OnLeft(-1);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _buttonWorkaroundInput.OnLeft(0);
        }
    }
}
