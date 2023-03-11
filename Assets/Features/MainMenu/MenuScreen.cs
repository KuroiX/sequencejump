using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.MainMenu
{
    public class MenuScreen : MonoBehaviour
    {
        private static Dictionary<string, MenuScreen> _screens;
        private static Stack<string> _stack;
        private static string _activeScreen;

        [SerializeField] private bool isStartScreen;

        private void Awake()
        {
            InitStatics();
            
            string myName = gameObject.name;
            
            _screens[myName] = this;

            if (_activeScreen == null && isStartScreen)
            {
                _activeScreen = myName;
            }

            gameObject.SetActive(String.Compare(_activeScreen, myName, StringComparison.Ordinal) == 0);
        }

        private void InitStatics()
        {
            _screens ??= new Dictionary<string, MenuScreen>();
            _stack ??= new Stack<string>();
        }

        public void Load(MenuScreen screen)
        {
            _stack.Push(gameObject.name);
            
            GameObject o = screen.gameObject;
            _activeScreen = o.name;
            o.SetActive(true);
            
            gameObject.SetActive(false);
        }
        
        public void Back()
        {
            if (_stack.Count == 0) return;
            
            string screen = _stack.Pop();
            _screens[screen].gameObject.SetActive(true);
            _activeScreen = _screens[screen].gameObject.name;
            
            gameObject.SetActive(false);
        }
    }
}