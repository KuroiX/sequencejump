using UnityEngine;

namespace Packages.GameJamUtility.Scripts
{
    public class AudioContainer : MonoBehaviour
    {
        public AudioClip BackgroundMusicClip => backgroundMusicClip;
    
        [SerializeField] private AudioClip backgroundMusicClip;
    }
}