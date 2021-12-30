using UnityEngine;
using UnityEngine.EventSystems;

namespace Features.Player.Controller
{
    public class RightButtonWorkaround : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    { 
        private ButtonWorkaroundInput _buttonWorkaroundInput;

        private void Awake()
        {
            _buttonWorkaroundInput = gameObject.GetComponentInParent<ButtonWorkaroundInput>();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
           _buttonWorkaroundInput.OnRight(1);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _buttonWorkaroundInput.OnRight(0);
        }
    }
}
