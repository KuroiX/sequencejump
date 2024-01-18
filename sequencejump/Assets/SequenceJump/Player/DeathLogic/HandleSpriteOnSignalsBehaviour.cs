using SequenceJump.Tools;
using UnityEngine;

namespace SequenceJump.Player.DeathLogic
{
    public class HandleSpriteOnSignalsBehaviour : MonoBehaviour
    {
        [SerializeField] private TimedSignalBehaviour signals;
        [SerializeField] private SpriteRenderer sprite;

        private void Awake()
        {
            sprite = sprite ? sprite : GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            signals.Stopped += DisableSprite;
            signals.Started += EnableSprite;
        }

        private void OnDisable()
        {
            signals.Stopped -= DisableSprite;
            signals.Started -= EnableSprite;
        }

        private void DisableSprite()
        {
            //sprite.enabled = false;
        }

        private void EnableSprite()
        {
            //sprite.enabled = true;
        }

    }
}