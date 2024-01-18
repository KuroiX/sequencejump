using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SequenceJump.MainMenu
{
    public class LoadLevelButton : MonoBehaviour
    {
        private LevelContainer _level;

        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.interactable = false;
        }

        public void LoadScene()
        {
            if (ReferenceEquals(_level, null)) return;
            
            SceneManager.LoadScene(_level.levelSceneName);
        }

        private void OnEnable()
        {
            LevelButton.LevelSelected += OnLevelSelected;
        }

        private void OnDisable()
        {
            LevelButton.LevelSelected -= OnLevelSelected;
        }

        private void OnLevelSelected(object sender, EventArgs args)
        {
            _level = ((LevelEventArgs) args).Level;
            _button.interactable = true;
        }
    }
}