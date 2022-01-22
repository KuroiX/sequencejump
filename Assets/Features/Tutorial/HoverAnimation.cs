using Core.Tools;
using UnityEngine;

namespace Features.Tutorial
{
    public class HoverAnimation : MonoBehaviour
    {
        [SerializeField] private Vector2 startPosition;
        [SerializeField] private Vector2 targetPosition;

        [SerializeField] private bool isRectTransform;

        private Transform _transform;
        
        private Vector2 Position
        {
            get => isRectTransform ? ((RectTransform) _transform).anchoredPosition : (Vector2)_transform.position;
            set
            {
                if (isRectTransform)
                {
                    ((RectTransform) _transform).anchoredPosition = value;
                }
                else
                {
                    _transform.position = new Vector3(value.x, value.y, _transform.position.z);
                }
                
            }
        }

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            StartCoroutine(TweenRoutines.Linear(startPosition, targetPosition, 0.5f, 
                result => { Position = result; },
                true));
        }

        [ContextMenu("SetStart")]
        private void SetStart()
        {
            _transform = transform;
            startPosition = Position;
        }

        [ContextMenu("SetTarget")]
        private void SetTarget()
        {
            _transform = transform;
            targetPosition = Position;
        }
    }
}