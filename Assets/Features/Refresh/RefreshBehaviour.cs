using System;
using Core.Actions;
using Core.Queue;
using Features.StationLogic;
using UnityEngine;

namespace Features.Refresh
{
    public class RefreshBehaviour : MonoBehaviour
    {
        [SerializeField] private StationBehaviour stationBehaviour;

        private ResettableQueue<ICharacterAction> _queue;
        private bool _isEnabled = true;
        private SpriteRenderer _spriteRenderer;
        
        private void Start()
        {
            _queue = FindObjectOfType<QueueHolder>().Queue;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            Station.StationEntered += OnRefreshRefresher;
        }

        private void OnRefreshRefresher(object sender, EventArgs e)
        {
            if (stationBehaviour.Station != sender) return;

            _isEnabled = true;
            _spriteRenderer.color = Color.green;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!_isEnabled) return;
            
            _queue.ResetQueue();
            SetDisabled();
        }

        private void SetDisabled()
        {
            _isEnabled = false;
            _spriteRenderer.color = Color.gray;
        }
        
    }
}