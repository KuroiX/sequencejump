using System;
using TMPro;
using UnityEngine;

namespace SequenceJump.MainMenu
{
    public class LevelNameText : MonoBehaviour
    {
        private TMP_Text _text;
        private LevelContainer _level;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _text.text = "";
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
            _text.text = _level.levelName;
        }
    }
}
