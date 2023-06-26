using Features.StationLogic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Features.LevelEditor
{
    public class LevelSaver : MonoBehaviour
    {
        [SerializeField] private Tilemap[] tilemaps;

        [ContextMenu("Save Level")]
        public void SaveLevel()
        {
            //Debug.Log("Save Level");
            
            LevelData level = ScriptableObject.CreateInstance<LevelData>();
            
            SaveTilemaps(level);
            SaveStations(level);
            
            // AssetDatabase.CreateAsset(level, "Assets/Features/LevelEditor/Levels/newLevel.asset");
            // AssetDatabase.SaveAssets();
            //
            // EditorUtility.FocusProjectWindow();
            // Selection.activeObject = level;
        }

        private void SaveTilemaps(LevelData level)
        {
            level.TilemapInfos = new TilemapInfo[tilemaps.Length];
            
            for (int i = 0; i < tilemaps.Length; i++)
            {
                level.TilemapInfos[i] = SaveTilemap(tilemaps[i]);
            }
        }

        private TilemapInfo SaveTilemap(Tilemap map)
        {
            TilemapInfo info = new TilemapInfo();
            
            map.CompressBounds();
            BoundsInt bounds = map.cellBounds;

            info.Bounds = bounds;
            info.Color = map.color;
            info.Tiles = map.GetTilesBlock(bounds);

            return info;
        }

        private void SaveStations(LevelData level)
        {
            var stations = FindObjectsOfType<StationBehaviour>();

            level.StationInfos = new StationInfo[stations.Length];

            for (int i = 0; i < stations.Length; i++)
            {
                level.StationInfos[i] = new StationInfo(stations[i]);
                Debug.Log(level.StationInfos[i].Position);
            }
        }
    }
}