using UnityEngine;
using UnityEngine.EventSystems;

namespace Features.Player.Controller
{
    public class RightButtonWorkaround : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    { 
        private ButtonWorkaround _buttonWorkaround;

        private void Awake()
        {
            _buttonWorkaround = gameObject.GetComponentInParent<ButtonWorkaround>();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
           _buttonWorkaround.OnRight(1);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _buttonWorkaround.OnRight(0);
        }
    }
}
