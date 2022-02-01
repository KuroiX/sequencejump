using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Features.MainMenu
{
    public class LoadLevelButton : MonoBehaviour
    {
        private LevelContainer _level;
        
        private void Awake()
        {
            //GetComponent<Button>().enabled = false;
        }

        public void OnButtonClick()
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
        }
    }
}