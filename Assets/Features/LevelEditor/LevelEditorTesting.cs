using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Features.LevelEditor
{
    public class LevelEditorTesting : MonoBehaviour
    {
        [SerializeField] private Tilemap groundMap;
        [SerializeField] private Tilemap hazardMap;
        [SerializeField] private Tilemap mapToWrite;

        [SerializeField] private LevelObject testLevel;

        [SerializeField] private GameObject prefab;
        
        private void Awake()
        {
            var gO = Instantiate(prefab);
            gO.GetComponent<TestInstantiator>().Set();
            gO.SetActive(true);
            
            return;
            
            // Debug.Log(testLevel.GroundBounds);
            // Debug.Log(testLevel.GroundTiles.Length);
            //
            // var bounds = testLevel.GroundBounds;
            // Debug.Log(bounds);
            // Debug.Log(bounds.x);
            // Debug.Log(bounds.xMin);
            // Debug.Log(bounds.xMax);
            //
            // groundMap.CompressBounds();
            // var test = testLevel.GroundTiles;
            // //var test2 = mapToRead.cellBounds.allPositionsWithin;
            // Debug.Log(test.Length);
            //
            //
            //
            // for (int i = 0; i < test.Length; i++)
            // {
            //     int x = i % bounds.size.x + bounds.xMin;
            //     int y = i / bounds.size.x + bounds.yMin;
            //     int z = bounds.z;
            //     
            //     mapToWrite.SetTile(new Vector3Int(x, y, z), test[i]);
            // }
        }
    }
}
