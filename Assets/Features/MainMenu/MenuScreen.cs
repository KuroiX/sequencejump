using System.Collections.Generic;
using UnityEngine;

namespace Features.MainMenu
{
    public class MenuScreen : MonoBehaviour
    {
        private static Stack<MenuScreen> _screens;

        private void Awake()
        {
            _screens ??= new Stack<MenuScreen>();
        }

        public void Load(MenuScreen screen)
        {
            _screens.Push(this);
            screen.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        
        public void Back()
        {
            if (_screens.Count == 0) return;
            _screens.Pop().gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}