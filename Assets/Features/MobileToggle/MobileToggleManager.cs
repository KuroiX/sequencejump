using UnityEngine;

namespace Features.MobileToggle
{
    public class MobileToggleManager : MonoBehaviour
    {
        public static MobileToggleManager Instance { get; private set; }

        public bool IsMobile { get; private set; }
        
        private void Awake()
        {
            if (Instance)
            {
                IsMobile = Instance.IsMobile;
                Destroy(Instance.gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(this);
        }

        public void OnMobileToggle(bool isToggled)
        {
            IsMobile = isToggled;
        }
    }
}
