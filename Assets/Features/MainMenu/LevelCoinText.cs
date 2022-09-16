using System;
using System.Text;
using TMPro;
using UnityEngine;

namespace Features.MainMenu
{
    public class LevelCoinText : MonoBehaviour
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
            StringBuilder builder = new StringBuilder();
            builder.Append("Coins: ");
            builder.Append(_level.collectedCoins);
            builder.Append(" / ");
            builder.Append(_level.availableCoins);
            
            _text.text = builder.ToString();
        }
    }
}
