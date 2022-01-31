using System;
using UnityEngine;

namespace Features.MainMenu
{
    public class LevelButton : MonoBehaviour
    {
        public static event EventHandler LevelSelected;
        
        [SerializeField] private GameObject selectionEnabled;
        [SerializeField] private GameObject selectionDisabled;

        [SerializeField] private LevelContainer level;

        private void Awake()
        {
            if (level.isUnlocked)
            {
                EnableSelection();
            }
            else
            {
                DisableSelection();
            }
        }

        private void OnLevelSelected()
        {
            LevelSelected?.Invoke(this, EventArgs.Empty);
        }

        private void EnableSelection()
        {
            selectionEnabled.SetActive(true);
        }
        
        private void DisableSelection()
        {
            selectionDisabled.SetActive(false);
        }

        public void SelectLevel()
        {
            OnLevelSelected();
        }
    }
}