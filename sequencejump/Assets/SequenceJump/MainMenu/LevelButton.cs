using System;
using UnityEngine;

namespace SequenceJump.MainMenu
{
    public class LevelButton : MonoBehaviour
    {
        public static event EventHandler LevelSelected;
        
        [SerializeField] private GameObject selectionEnabled;
        [SerializeField] private GameObject selectionDisabled;

        [SerializeField] private LevelContainer level;

        private void Awake()
        {
            if (ReferenceEquals(level, null)) return;
            
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
            LevelSelected?.Invoke(this, new LevelEventArgs(level));
        }

        private void EnableSelection()
        {
            selectionEnabled.SetActive(true);
        }
        
        private void DisableSelection()
        {
            selectionDisabled.SetActive(true);
        }

        public void SelectLevel()
        {
            OnLevelSelected();
        }
    }
}