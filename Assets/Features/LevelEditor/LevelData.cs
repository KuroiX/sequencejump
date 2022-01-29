using UnityEngine;

namespace Features.LevelEditor
{
    [CreateAssetMenu(fileName = "level", menuName = "ScriptableObjects/Level", order = 0)]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private TilemapInfo[] tilemapInfos;
        [SerializeField] private StationInfo[] stationInfos;

        public TilemapInfo[] TilemapInfos { get => tilemapInfos; set => tilemapInfos = value; }
        
        public StationInfo[] StationInfos { get => stationInfos; set => stationInfos = value; }
    }
}