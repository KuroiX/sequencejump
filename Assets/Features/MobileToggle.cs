using System;
using UnityEngine;

namespace Features
{
    [CreateAssetMenu(fileName = "mobile_toggle", menuName = "ScriptableObject/MobileToggle", order = 0)]
    public class MobileToggle : ScriptableObject
    {
        public MobileToggle Instance => _instance;

        public bool IsMobile { get; private set; }

        private MobileToggle _instance;

        private void Awake()
        {
            _instance = this;
        }

        public void OnMobileToggle(bool isToggled)
        {
            IsMobile = isToggled;
        }
    }
}
