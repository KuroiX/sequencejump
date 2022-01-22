using System;
using UnityEngine;

namespace Features.LevelEditor
{
    [Serializable]
    public class StationInfo
    {
        public Vector3 Position { get; set; }
        public int MaxAssignableCounts { get; set; }
        public string[] ActionNames { get; set; }
        public int[] ActionCounts { get; set; }
        
        public Vector3 CameraPosition { get; set; }
        public int CameraSize { get; set; }
    }
}