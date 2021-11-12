﻿using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Station
{
    [System.Serializable]
    public struct StationSettings
    {
        public int maxAssignableActions;
        [SerializeField] public ActionCount[] actionCounts;
    }
}