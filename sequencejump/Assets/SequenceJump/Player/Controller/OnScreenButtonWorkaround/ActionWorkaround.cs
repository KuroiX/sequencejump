using UnityEngine;
using UnityEngine.EventSystems;

namespace SequenceJump.Player.Controller.OnScreenButtonWorkaround
{
    public class ActionWorkaround : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    { 
        private ButtonWorkaroundInput _buttonWorkaroundInput;

        private void Awake()
        {
            _buttonWorkaroundInput = gameObject.GetComponentInParent<ButtonWorkaroundInput>();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _buttonWorkaroundInput.OnActionDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _buttonWorkaroundInput.OnActionUp();
        }
    }
}
