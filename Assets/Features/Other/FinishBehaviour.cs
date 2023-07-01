using Features.GameJamUtility.Scripts;
using UnityEngine;

namespace Features.Other
{
    public class FinishBehaviour : MonoBehaviour
    {
        [SerializeField] private SceneLoader sceneLoader;

        private void Awake()
        {
            sceneLoader ??= FindObjectOfType<SceneLoader>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            sceneLoader.LoadSceneByIndex(0);
        }
    }
}