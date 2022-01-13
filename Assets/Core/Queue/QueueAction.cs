using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Queue
{
    public class QueueAction : MonoBehaviour
    {
        public Sprite Sprite
        {
            set => image.sprite = value;
        }

        [SerializeField] private Image image;

        [SerializeField] private int index;

        private int _currentIndex;

        private static Vector2[] _targetPositions;
        private static Vector2[] _targetSizes;
        private static float[] _targetAlpha;

        private RectTransform _transform;

        private void Awake()
        {
            _transform = (RectTransform) transform;
            
            _targetPositions ??= new Vector2[5];
            _targetSizes ??= new Vector2[5];
            _targetAlpha ??= new float[5];
            
            _targetPositions[index] = _transform.anchoredPosition;
            _targetSizes[index] = _transform.sizeDelta;
            _targetAlpha[index] = image.color.a;

            _currentIndex = index;
        }

        public void StartAnimation()
        {
            StopAllCoroutines();
            StartCoroutine(AnimationRoutine(0.2f));
        }
        
        private IEnumerator AnimationRoutine(float animationTime)
        {
            int oldIndex = _currentIndex;
            _currentIndex = (_currentIndex - 1 + 5) % 5;
            
            Vector2 direction = _targetPositions[_currentIndex] - _targetPositions[oldIndex];
            Vector2 growthDirection = _targetSizes[_currentIndex] - _targetSizes[oldIndex];
            
            float alpha = _targetAlpha[_currentIndex] - _targetAlpha[oldIndex];
            
            float elapsedTime = 0f;

            while (true)
            {
                yield return null;
                elapsedTime += Time.deltaTime;
                //Debug.Log(elapsedTime);

                bool reachedEnd = elapsedTime >= animationTime;

                elapsedTime = reachedEnd ? animationTime : elapsedTime;

                Vector2 newPosition = _targetPositions[oldIndex] + direction * elapsedTime / animationTime;
                Vector2 newSize = _targetSizes[oldIndex] + growthDirection * elapsedTime / animationTime;
                float newAlpha = _targetAlpha[oldIndex] + alpha * elapsedTime / animationTime;
                
                //Debug.Log(result);
                
                _transform.anchoredPosition = newPosition;
                _transform.sizeDelta = newSize;
                image.color = new Color(1, 1, 1, newAlpha);

                if (reachedEnd) break;
            }
        }
    }
}