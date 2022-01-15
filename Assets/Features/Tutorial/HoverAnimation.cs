using System;
using System.Collections;
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
            StartCoroutine(AnimationRoutine(0.5f));
        }

        private IEnumerator AnimationRoutine(float animationTime)
        {
            //Debug.Log(index + " " + from + " " + to);
            Vector2 direction = targetPosition - startPosition;
            
            float elapsedTime = 0f;

            Vector2 position = startPosition;

            bool isGoingForward = true;
            
            while (true)
            {
                yield return null;
                elapsedTime += Time.deltaTime;

                bool reachedEnd = elapsedTime >= animationTime;

                elapsedTime = reachedEnd ? animationTime : elapsedTime;

                Vector2 newPosition = position + direction * elapsedTime / animationTime;

                Position = newPosition;

                if (reachedEnd)
                {
                    elapsedTime = 0;
                    isGoingForward = !isGoingForward;
                    position = isGoingForward ? startPosition : targetPosition;
                    direction *= -1;
                }
            }
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