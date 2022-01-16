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
            set
            {
                image.sprite = value;
                image.enabled = !ReferenceEquals(value, null);
            }
        }

        [SerializeField] private Image image;

        [SerializeField] private int index;

        private static Vector2[] _targetPositions;
        private static Vector2[] _targetSizes;
        private static float[] _targetAlpha;

        private RectTransform _transform;

        private void Awake()
        {
            _transform = (RectTransform) transform;
            
            _targetPositions ??= new Vector2[6];
            _targetSizes ??= new Vector2[6];
            _targetAlpha ??= new float[6];
            
            _targetPositions[index] = _transform.anchoredPosition;
            _targetSizes[index] = _transform.sizeDelta;
            _targetAlpha[index] = image.color.a;
        }

        public void StartAnimation(int from, int to)
        {
            StopAllCoroutines();
            StartCoroutine(AnimationRoutine(from, to, 0.2f));
        }

        public void AnimateEnqueue(int at)
        {
            StartCoroutine(EnqueueRoutine(at,0.1f));
        }

        public void AnimateReset(int at)
        {
            StartCoroutine(ResetRoutine(at,0.1f));
        }

        public void SetEmpty()
        {
            image.sprite = null;
            image.enabled = false;
        }
        
        private IEnumerator AnimationRoutine(int from, int to, float animationTime)
        {
            //Debug.Log(index + " " + from + " " + to);
            Vector2 direction = _targetPositions[to] - _targetPositions[from];
            Vector2 growthDirection = _targetSizes[to] - _targetSizes[from];
            float alpha = _targetAlpha[to] - _targetAlpha[from];
            
            float elapsedTime = 0f;

            while (true)
            {
                yield return null;
                elapsedTime += Time.deltaTime;
                //Debug.Log(elapsedTime);

                bool reachedEnd = elapsedTime >= animationTime;

                elapsedTime = reachedEnd ? animationTime : elapsedTime;

                Vector2 newPosition = _targetPositions[from] + direction * elapsedTime / animationTime;
                Vector2 newSize = _targetSizes[from] + growthDirection * elapsedTime / animationTime;
                float newAlpha = _targetAlpha[from] + alpha * elapsedTime / animationTime;
                
                //Debug.Log(result);
                
                _transform.anchoredPosition = newPosition;
                _transform.sizeDelta = newSize;
                image.color = new Color(1, 1, 1, newAlpha);

                if (reachedEnd) break;
            }
        }
        
        private IEnumerator EnqueueRoutine(int at, float animationTime)
        {
            Vector2 growthDirection = _targetSizes[at] - Vector2.zero;
            
            float elapsedTime = 0f;

            while (true)
            {
                elapsedTime += Time.deltaTime;
                //Debug.Log(elapsedTime);

                bool reachedEnd = elapsedTime >= animationTime;

                elapsedTime = reachedEnd ? animationTime : elapsedTime;

                Vector2 newSize = Vector2.zero + growthDirection * elapsedTime / animationTime;
                
                //Debug.Log(result);
                
                _transform.sizeDelta = newSize;

                if (reachedEnd) break;
                
                yield return null;
            }
        }
        
        private IEnumerator ResetRoutine(int at, float animationTime)
        {
            Vector2 growthDirection = Vector2.zero - _targetSizes[at];
            
            float elapsedTime = 0f;

            while (true)
            {
                elapsedTime += Time.deltaTime;
                //Debug.Log(elapsedTime);

                bool reachedEnd = elapsedTime >= animationTime;

                elapsedTime = reachedEnd ? animationTime : elapsedTime;

                Vector2 newSize = _targetSizes[at] + growthDirection * elapsedTime / animationTime;
                
                //Debug.Log(result);
                
                _transform.sizeDelta = newSize;

                if (reachedEnd) break;
                
                yield return null;
            }

            _transform.sizeDelta = _targetSizes[at];
            Sprite = null;
        }
    }
}