using UnityEngine;
using UnityEngine.UI;

namespace Features.MobileToggle
{
    public class MobileToggleSetter : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Toggle>().isOn = MobileToggleManager.Instance.IsMobile;
        }
    }
}