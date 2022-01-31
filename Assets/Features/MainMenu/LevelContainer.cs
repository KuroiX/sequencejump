﻿using UnityEngine;

namespace Features.MainMenu
{
    [CreateAssetMenu(fileName = "level", menuName = "ScriptableObjects/LevelContainer", order = 0)]
    public class LevelContainer : ScriptableObject
    {
        public LevelContainer prevLevel;
        public LevelContainer nextLevel;
        
        public bool isUnlocked;
        public bool isCleared;
        public string levelName;
        public string levelSceneName;
    }
}