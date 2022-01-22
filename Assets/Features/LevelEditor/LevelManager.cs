using Features.StationLogic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Features.LevelEditor
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Tilemap[] tilemaps;

        [SerializeField] private LevelObject levelToLoad;
        

        [ContextMenu("Save Level")]
        public void SaveLevel()
        {
            //Debug.Log("Save Level");
            
            LevelObject level = ScriptableObject.CreateInstance<LevelObject>();
            
            SaveTilemaps(level);
            SaveStations(level);
            
            AssetDatabase.CreateAsset(level, "Assets/Features/LevelEditor/newLevel.asset");
            AssetDatabase.SaveAssets();
            
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = level;
        }

        private void SaveTilemaps(LevelObject level)
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

        private void SaveStations(LevelObject level)
        {
            var stations = FindObjectsOfType<StationBehaviour>();

            level.StationInfos = new StationInfo[stations.Length];

            for (int i = 0; i < stations.Length; i++)
            {
                StationInfo info = new StationInfo();

                info.Position = stations[i].transform.position;
                //info.MaxAssignableCounts = stations[i].max
            }
        }

        [ContextMenu("Load Level")]
        public void LoadLevel()
        {
            Debug.Log("Load Level");
            
            LoadTilemaps(levelToLoad);
        }

        private void LoadTilemaps(LevelObject level)
        {
            for (int i = 0; i < level.TilemapInfos.Length; i++)
            {
                SetTilemap(level.TilemapInfos[i], tilemaps[i]);
            }
        }

        private void SetTilemap(TilemapInfo info, Tilemap map)
        {
            Debug.Log(info.Bounds);
            for (int i = 0; i < info.Tiles.Length; i++)
            {
                int x = i % info.Bounds.size.x + info.Bounds.xMin;
                int y = i / info.Bounds.size.x + info.Bounds.yMin;
                int z = info.Bounds.z;
                
                map.SetTile(new Vector3Int(x, y, z), info.Tiles[i]);
            }

            map.color = info.Color;
        }
    }
    
    // [CustomEditor(typeof(LevelManager))]
    // public class TestOnInspector : Editor
    // {
    //     public override void OnInspectorGUI()
    //     {
    //         base.OnInspectorGUI();
    //
    //         LevelManager myTarget = (LevelManager) target;
    //         
    //         GUILayout.Space(10);
    //         
    //         if (GUILayout.Button("Load Level"))
    //         {
    //             myTarget.LoadLevel();
    //         }
    //         
    //         if (GUILayout.Button("Save Level"))
    //         {
    //             myTarget.SaveLevel();
    //         }
    //     }
    // }
}