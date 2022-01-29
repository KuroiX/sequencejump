using System;
using Features.StationLogic;
using UnityEngine;

namespace Features.LevelEditor
{
    [Serializable]
    public struct StationInfo
    {
        public Vector3 Position { get; }
        public int MaxAssignableCount { get; }
        public int[] ActionCounts { get; }
        
        public Vector3 CameraPosition { get; }
        public float CameraSize { get; }

        public StationInfo(StationBehaviour station)
        {
            Position = station.transform.position;
            MaxAssignableCount = station.MaxAssignableCount;
            
            int length = station.ActionCounts.Length;
            Debug.Log(length);
            ActionCounts = new int[length];
            Array.Copy(station.ActionCounts, ActionCounts, length);

            CameraPosition = station.StationCamera.transform.position;
            CameraSize = station.StationCamera.m_Lens.OrthographicSize;
        }
    }
}