using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.MobileToggle
{
    public class MobileToggleSetter : MonoBehaviour
    {
        private void Awake()
        {
#if UNITY_ANDROID
            transform.parent.gameObject.SetActive(false);
#endif            
        }

        private void Start()
        {
            GetComponent<Toggle>().isOn = MobileToggleManager.Instance.IsMobile;
        }
    }
}