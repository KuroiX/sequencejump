using System;
using UnityEngine;

namespace Features.Player.DeathLogic
{
    public class HandleSpriteOnSignalsBehaviour : MonoBehaviour
    {
        [SerializeField] private Component signals;
        [SerializeField] private SpriteRenderer sprite;

        private IStopStartSignal _signals;

        private void Awake()
        {
            _signals = (IStopStartSignal) signals;
            sprite = sprite ? sprite : GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _signals.Stop += DisableSprite;
            _signals.Start += EnableSprite;
        }

        private void OnDisable()
        {
            _signals.Stop -= DisableSprite;
            _signals.Start -= EnableSprite;
        }

        private void DisableSprite(object sender, EventArgs args)
        {
            sprite.enabled = false;
        }

        private void EnableSprite(object sender, EventArgs args)
        {
            sprite.enabled = true;
        }

    }
}