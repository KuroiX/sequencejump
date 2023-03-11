using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Features.LevelEditor
{
    [Serializable]
    public class TilemapInfo
    {
        public BoundsInt Bounds { get; set; }
        public TileBase[] Tiles { get; set; }
        public Color Color { get; set; }

        public TilemapInfo()
        {
            
        }
    }
}