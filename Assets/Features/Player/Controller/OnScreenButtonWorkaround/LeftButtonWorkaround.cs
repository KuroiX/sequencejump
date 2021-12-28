using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Features.Player.Controller
{
    public class LeftButtonWorkaround : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private ButtonWorkaround _buttonWorkaround;

        private void Awake()
        {
            _buttonWorkaround = gameObject.GetComponentInParent<ButtonWorkaround>();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _buttonWorkaround.OnLeft(-1);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _buttonWorkaround.OnLeft(0);
        }
    }
}
