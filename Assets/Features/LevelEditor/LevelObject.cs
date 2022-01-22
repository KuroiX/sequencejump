using UnityEngine;
using UnityEngine.Tilemaps;

namespace Features.LevelEditor
{
    [CreateAssetMenu(fileName = "level", menuName = "ScriptableObjects/Level", order = 0)]
    public class LevelObject : ScriptableObject
    {
        public TilemapInfo[] TilemapInfos { get; set; }

        public StationInfo[] StationInfos { get; set; }
    }
}