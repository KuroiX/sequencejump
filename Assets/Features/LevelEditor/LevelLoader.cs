using Cinemachine;
using Features.StationLogic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Features.LevelEditor
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Tilemap[] tilemaps;

        [SerializeField] private LevelObject levelToLoad;
        [SerializeField] private GameObject stationPrefab;

        [Header("Injecting")] 
        [SerializeField] private CinemachineVirtualCamera mainCamera;


        [ContextMenu("Load Level")]
        public void LoadLevel()
        {
            //Debug.Log("Load Level");
            
            LoadTilemaps(levelToLoad.TilemapInfos);
            LoadStations(levelToLoad.StationInfos);
        }

        private void LoadTilemaps(TilemapInfo[] infos)
        {
            for (int i = 0; i < infos.Length; i++)
            {
                SetTilemap(infos[i], tilemaps[i]);
            }
        }

        private void SetTilemap(TilemapInfo info, Tilemap map)
        {
            //Debug.Log(info.Bounds);
            for (int i = 0; i < info.Tiles.Length; i++)
            {
                int x = i % info.Bounds.size.x + info.Bounds.xMin;
                int y = i / info.Bounds.size.x + info.Bounds.yMin;
                int z = info.Bounds.z;
                
                map.SetTile(new Vector3Int(x, y, z), info.Tiles[i]);
            }

            map.color = info.Color;
        }

        private void LoadStations(StationInfo[] infos)
        {
            for (int i = 0; i < infos.Length; i++)
            {
                LoadStation(infos[i]);
            }
        }

        private void LoadStation(StationInfo info)
        {
            Debug.Log(info.Position);
            GameObject station = Instantiate(stationPrefab, info.Position, Quaternion.identity);
            var behaviour = station.GetComponent<StationBehaviour>();
            var virtualCamera = station.GetComponentInChildren<CinemachineVirtualCamera>();
            
            behaviour.Construct(mainCamera, info.ActionCounts, info.MaxAssignableCount);

            virtualCamera.transform.position = info.CameraPosition;
            virtualCamera.m_Lens.OrthographicSize = info.CameraSize;
        }
    }
}