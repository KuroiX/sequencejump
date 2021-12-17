﻿using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Actions
{
    [Serializable]
    public struct ActionCounter
    {
        [HideInInspector]
        public CharacterAction characterAction;
        public int count;
    }
}